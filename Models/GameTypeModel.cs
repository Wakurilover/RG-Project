using Google.Cloud.Firestore;
namespace RandomG.Models;


public class GameTypeModel
{

    [FirestoreProperty]
    public string ID { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }


}