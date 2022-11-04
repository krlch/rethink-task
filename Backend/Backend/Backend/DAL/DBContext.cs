using Backend.DAL.Entities;

namespace Backend.DAL
{
    public static class DBContext
    {
        public static List<Contact> Contacts { get; set; }
        public static string PathToFile { get; set; }
        static DBContext()
        {
            Contacts = new List<Contact>();
        }
    }
}
