using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R4._01_TP4.Controllers;
using R4._01_TP4.Models.DataManager;
using R4._01_TP4.Models.EntityFramework;
using R4._01_TP4.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace R4._01_TP4.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private FilmsRatingDBContext dbContext;
        private UtilisateursController utilisateursController;
        private IDataRepository<Utilisateur> dataRepository;

        [TestInitialize]
        public void Init()
        {
            //dbContext = new FilmsRatingDBContext();
            var builder = new DbContextOptionsBuilder<FilmsRatingDBContext>().UseNpgsql("FilmRatingsDBContext");
            dbContext = new FilmsRatingDBContext(builder.Options);
            dataRepository = new UtilisateurManager(dbContext);
            //utilisateursController = new UtilisateursController(dbContext);
            utilisateursController = new UtilisateursController(dataRepository);
        }

        [TestMethod()]
        public void UtilisateursControllerTest()
        {
            var utilisateurController = new UtilisateursController(dataRepository);
            Assert.IsNotNull(utilisateurController);
            Assert.IsInstanceOfType(utilisateurController, typeof(UtilisateursController));
        }

        [TestMethod()]
        public void GetUtilisateursTest_ReturnsOK()
        {
            var utilisateursBase = dbContext.Utilisateurs.ToList();
            var utilisateursGetAll = utilisateursController.GetUtilisateurs();

            CollectionAssert.AreEquivalent(utilisateursBase, utilisateursGetAll.Result.Value.ToList());
        }

        //[TestMethod()]
        //public void GetUtilisateurByIdTest_ReturnsOK()
        //{
        //    var utilisateurBase = dbContext.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();
        //    var getUtilisateur = utilisateursController.GetUtilisateurById(1).Result.Value;

        //    Assert.AreEqual(utilisateurBase, getUtilisateur, "Erreur, utilisateur non correspondant");
        //}

        [TestMethod()]
        public void GetUtilisateurByIdTest_ReturnsOK_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };

            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);  
            
            var userController = new UtilisateursController(mockRepository.Object);

            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }


        //[TestMethod()]
        //public void GetUtilisateurByIdTest_ReturnsNotFound()
        //{
        //    var getUtilisateur = utilisateursController.GetUtilisateurById(500);
        //    Assert.AreEqual(getUtilisateur.Result.Result, null, "Erreur, pas un NotFoundResult");
        //}

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_ReturnsOK()
        {
            var utilisateurBase = dbContext.Utilisateurs.Where(c => c.Mail == "clilleymd@last.fm").FirstOrDefault();
            var getUtilisateur = utilisateursController.GetUtilisateurByEmail("clilleymd@last.fm").Result;

            Assert.AreEqual(utilisateurBase, getUtilisateur.Value, "Erreur, utilisateur non correspondant");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_ReturnsOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);

            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            mockRepository.Setup(x => x.GetByStringAsync("poisson@gmail.com").Result).Returns(user);

            // Act
            var actionResult = userController.GetUtilisateurByEmail("poisson@gmail.com").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur, "Erreur, utilisateur non correspondant");
        }

        //[TestMethod()]
        //public void GetUtilisateurByEmailTest_ReturnsNotFound()
        //{
        //    var getUtilisateur = utilisateursController.GetUtilisateurByEmail("totototo@last.fm").Result;

        //    Assert.IsInstanceOfType(getUtilisateur.Result, typeof(NotFoundResult), "Erreur, pas un NotFound");
        //}

        [TestMethod()]
        public void GetUtilisateurByEmailTest_ReturnsNotFound_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);

            var actionResult = userController.GetUtilisateurByEmail("toto_du_bresil@gmail.com").Result;

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Erreur, pas un NotFound");
        }

        //[TestMethod]
        //public void Postutilisateur_ModelValidated_CreationOK()
        //{
        //    // Arrange
        //    Random rnd = new Random();
        //    int chiffre = rnd.Next(1, 1000000000);
        //    // Le mail doit être unique donc 2 possibilités :
        //    // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
        //    // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
        //    Utilisateur userAtester = new Utilisateur()
        //    {
        //        Nom = "MACHIN",
        //        Prenom = "Luc",
        //        Mobile = "0606070809",
        //        Mail = "machin" + chiffre + "@gmail.com",
        //        Pwd = "Toto1234!",
        //        Rue = "Chemin de Bellevue",
        //        CodePostal = "74940",
        //        Ville = "Annecy-le-Vieux",
        //        Pays = "France",
        //        Latitude = null,
        //        Longitude = null
        //    };
        //    // Act
        //    var result = utilisateursController.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
        //    // Assert
        //    Utilisateur? userRecupere = dbContext.Utilisateurs.Where(u => u.Mail.ToUpper() ==
        //    userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
        //    // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
        //    // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
        //    userAtester.UtilisateurId = userRecupere.UtilisateurId;
        //    Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");

        //}

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            //Utilisateur userAtester = new Utilisateur()
            //{
            //    Nom = "MACHIN",
            //    Prenom = "Luc",
            //    Mobile = "0606070809",
            //    Mail = "machin" + chiffre + "@gmail.com",
            //    Pwd = "Toto1234!",
            //    Rue = "Chemin de Bellevue",
            //    CodePostal = "74940",
            //    Ville = "Annecy-le-Vieux",
            //    Pays = "France",
            //    Latitude = null,
            //    Longitude = null
            //};

            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }


        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "Bidule",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            dbContext.Add(userAtester);
            dbContext.SaveChanges();
            var users = dbContext.Utilisateurs.ToList();
            Utilisateur lastUser = users.OrderByDescending(p => p.UtilisateurId).FirstOrDefault();
            int lastIndex = lastUser.UtilisateurId;

            var result = utilisateursController.DeleteUtilisateur(lastIndex).Result;
            Utilisateur userDelete = utilisateursController.GetUtilisateurById(lastIndex).Result.Value;

            Assert.IsNull(userDelete);
        }

        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PutUtilisateurTest_AvecMoq()
        {
            Utilisateur userBase = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            Utilisateur userModif = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Pedro",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(userBase);
            var userController = new UtilisateursController(mockRepository.Object);

            // Act
            var actionResult = userController.PutUtilisateur(1, userModif).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour


        }
    }
}