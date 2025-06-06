
using Plugin.Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiKit.Models
{
    public class UserModel: IFirestoreObject
    {
        [FirestoreDocumentId] // Gán Document ID từ Firestore
        public string Id { get; set; }

        [FirestoreProperty("name")]
        public string Name { get; set; }

        [FirestoreProperty("email")]
        public string Email { get; set; }

        [FirestoreProperty("phone")]
        public string Phone { get; set; }

        [FirestoreProperty("photoUrl")]
        public string PhotoUrl { get; set; }

        [FirestoreProperty("role")]
        public string Role { get; set; }

        [FirestoreProperty("location")]
        public LocationModel Location { get; set; }

        [FirestoreProperty("following")]
        public List<string> Following { get; set; } = new();

        [FirestoreProperty("followers")]
        public List<string> Followers { get; set; } = new();
    }

    public class LocationModel : IFirestoreObject
    {
        [FirestoreProperty("lat")]
        public double Latitude { get; set; }

        [FirestoreProperty("lng")]
        public double Longitude { get; set; }

        [FirestoreProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    public class LocationHistoryEntry
    {
        [FirestoreProperty("lat")]
        public double Latitude { get; set; }

        [FirestoreProperty("lng")]
        public double Longitude { get; set; }

        [FirestoreProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
