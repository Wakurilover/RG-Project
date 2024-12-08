using Google.Cloud.Firestore;
using RandomG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RandomG.Services
{
    public class FirestoreService
    {
        private FirestoreDb db;
        public string StatusMessage;

        public FirestoreService()
        {
            //this.SetupFireStore();
        }
        private async Task EnsureInitializedAsync()
        {
            if (db == null)
            {
                await SetupFireStore();
            }
        }
        private async Task SetupFireStore()
        {
            if (db == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("random-g-e9caa-firebase-adminsdk-kkavu-d5bf0a542e.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();
                db = new FirestoreDbBuilder
                {
                    ProjectId = "random-g-e9caa",

                    JsonCredentials = contents
                }.Build();
            }

            //var _gameType = await GetAllGameType();
            //var _gameCategories = await GetAllCategories();
            //var _gamerandom = await GetAllGameForRamdom("ACTION", new List<string> { "PC" });
            //string gameToPlay = _gamerandom.Name;

        }


        public async Task<GameModel> GetAllGameForRamdom(string CategoriesID, List<string> GameType)
        {
            try
            {

                var _game = await GetAllGame();
                var _gameForRamdom = _game.Where(e => e.CategoriesID == CategoriesID && e.GameTypeID.Any(type => GameType.Contains(type)))
                    .ToList();

                Random random = new Random();

                if (_gameForRamdom.Any())
                {
                    var entity = _gameForRamdom[random.Next(_gameForRamdom.Count)];
                    return entity;
                }



                return new GameModel();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public async Task<List<GameTypeModel>> GetAllGameType()
        {
            try
            {
                await EnsureInitializedAsync();
                var data = await db.Collection("GameType").GetSnapshotAsync();
                var entity = data.Documents.Select(doc =>
                {
                    var entity = new GameTypeModel();
                    entity.ID = doc.GetValue<string>("ID");
                    entity.Name = doc.GetValue<string>("Name");
                    return entity;
                }).ToList();
                return entity;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }
        public async Task<List<CategoriesModel>> GetAllCategories()
        {
            try
            {
                await EnsureInitializedAsync();
                var data = await db.Collection("Categories").GetSnapshotAsync();
                var entity = data.Documents.Select(doc =>
                {
                    var entity = new CategoriesModel();
                    entity.ID = doc.GetValue<string>("ID");
                    entity.Name = doc.GetValue<string>("Name");
                    return entity;
                }).ToList();
                return entity;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }
        public async Task<List<GameModel>> GetAllGame()
        {
            try
            {
                await EnsureInitializedAsync();
                var data = await db.Collection("Game").GetSnapshotAsync();
                var entity = data.Documents.Select(doc =>
                {
                    var entity = new GameModel();
                    entity.ID = doc.GetValue<int>("ID");
                    entity.CategoriesID = doc.GetValue<string>("CategoriesID");

                    // Get GameTypeID as List<string>
                    var gameTypeList = doc.GetValue<List<object>>("GameTypeID");
                    entity.GameTypeID = gameTypeList?.Select(obj => obj.ToString()).ToList();

                    entity.Name = doc.GetValue<string>("Name");
                    
                    ///////////////entity.Image = doc.GetValue<string>("image");
                    return entity;
                }).ToList();
                return entity;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }


    }
}