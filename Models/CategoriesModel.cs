using Google.Cloud.Firestore;
namespace RandomG.Models;


public class CategoriesModel
{

    [FirestoreProperty]
    public string ID { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }


}