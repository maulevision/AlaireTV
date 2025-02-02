using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AlaireTV
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Ejemplo: "Admin", "User"

        // Método para validar credenciales
        public static bool ValidateCredentials(string username, string password, List<User> users)
        {
            return users.Exists(u => u.Username == username && u.Password == password);
        }

        // Método para cargar usuarios desde un archivo JSON
        public static List<User> LoadUsers(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }

        // Método para guardar usuarios en un archivo JSON
        public static void SaveUsers(List<User> users, string filePath)
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(filePath, json);
        }
    }
}