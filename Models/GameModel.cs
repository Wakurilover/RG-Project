using Google.Cloud.Firestore;
namespace RandomG.Models;


public class GameModel
{

    [FirestoreProperty]
    public int ID { get; set; }

    [FirestoreProperty]
    public string CategoriesID { get; set; }

    [FirestoreProperty]
    public List<string> GameTypeID { get; set; }

    public string Name { get; set; }

    //public string GameTypeIDString { get; set; }

    //////////////////public string Image { get; set; }

}