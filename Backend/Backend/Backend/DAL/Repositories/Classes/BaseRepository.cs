using Backend.DAL.Entities;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Backend.BL.utils;
using Backend.BL.Enums;
using Backend.PL.Extensions;
using Backend.PL.Exceptions;
using Backend.BL.Services.Interfaces;
using Backend.PL.Controllers;

namespace Backend.DAL.Repositories.Classes
{
    public abstract class BaseRepository
    {
        

        //Reading all file with database every request is bad idea in case if we have big amount of stored entities.
        //In this case we can read line by line and construct object in this way.
        //But document could be stored in one line
        //In this case we can read file by parts and go through string and parse it 
        public ServiceResult<List<Contact>> GetAllObjects()
        {
            string text = File.ReadAllText(DBContext.PathToFile);
            if (text.Length < 5)
            {
                text = "[]";
                
                File.WriteAllText(DBContext.PathToFile, text);

            }

            var contacts = JsonConvert.DeserializeObject<List<Contact>>(text);

            if (contacts is null)
            {

                throw new ApplicationHelperException(ServiceResultType.InvalidData, "Couldn't extract data from db");
            }

            return new() { Result = ServiceResultType.Ok, Data = contacts };
        }

        public ServiceResult<List<Contact>> SaveDatabase(List<Contact> contacts)
        {
            var text = JsonConvert.SerializeObject(contacts);

            File.WriteAllText(DBContext.PathToFile, text);

            return new(ServiceResultType.Ok);
        }

    }
}
