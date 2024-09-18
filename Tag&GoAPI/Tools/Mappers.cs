using Tag_Go.DAL.Entities;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Models;

namespace Tag_GoAPI.Tools
{
    public static class Mappers
    {
    #nullable disable
        public static Activity ActivityToDal(this ActivityRegisterForm arf)
        {
            return new Activity
            {
                ActivityName = arf.ActivityName,
                ActivityAddress = arf.ActivityAddress,
                ActivityDescription = arf.ActivityDescription,
                ComplementareInformation = arf.ComplementareInformation,
                PosLat = arf.PosLat,
                PosLong = arf.PosLong,
                Organisateur_Id = arf.Organisateur_Id,
            };
        }
        public static Activity ActivityUpdateToDal(this ActivityUpdate arf)
        {
            return new Activity
            {
                Activity_Id = arf.Activity_Id,
                ActivityName = arf.ActivityName,
                ActivityAddress = arf.ActivityAddress,
                ActivityDescription = arf.ActivityDescription,
                ComplementareInformation = arf.ComplementareInformation,
                PosLat = arf.PosLat,
                PosLong = arf.PosLong,
                Organisateur_Id = arf.Organisateur_Id,
            };
        }
        public static Avatar AvatarToDal(this AvatarRegisterForm avrf)
        {
            return new Avatar
            {
                AvatarName = avrf.AvatarName,
                AvatarUrl = avrf.AvatarUrl,
                Description = avrf.Description,
            };
        }
        public static Avatar AvatarUpdateToDal(this AvatarUpdate avrf)
        {
            return new Avatar
            {
                Avatar_Id = avrf.Avatar_Id,
                AvatarName = avrf.AvatarName,
                AvatarUrl = avrf.AvatarUrl,
                Description = avrf.Description,
            };
        }
        public static Bonus BonusToDal(this BonusRegisterForm bonus)
        {
            return new Bonus
            {
                BonusType = bonus.BonusType,
                BonusDescription = bonus.BonusDescription,
                Application = bonus.Application,
                Granted = bonus.Granted,
            };
        }
        public static Bonus BonusUpdateToDal(this BonusUpdate bonus)
        {
            return new Bonus
            {
                Bonus_Id = bonus.Bonus_Id,
                BonusType = bonus.BonusType,
                BonusDescription = bonus.BonusDescription,
                Application = bonus.Application,
                Granted = bonus.Granted,
            };
        }
        public static Chat ChatToDal(this MessageModel ch)
        {
            return new Chat
            {
                NewMessage = ch.NewMessage,
                Author = ch.Author,
                SendingDate = ch.SendingDate,
                Evenement_Id = ch.Evenement_Id,
                Activity_Id = ch.Activity_Id
            };
        }
        public static Map MapToDal(this MapRegisterForm map)
        {
            return new Map
            {
                DateCreation = map.DateCreation,
                MapUrl = map.MapUrl,
                Description = map.Description,
            };
        }
        public static Map MapUpdateToDal(this MapUpdate map)
        {
            return new Map
            {
                Map_Id = map.Map_Id,
                DateCreation = map.DateCreation,
                MapUrl = map.MapUrl,
                Description = map.Description,
            };
        }
        public static MediaItem MediaItemToDal(this MediaItemRegisterForm mirf)
        {
            return new MediaItem
            {
                MediaType = mirf.MediaType,
                UrlItem = mirf.UrlItem,
                Description = mirf.Description,
            };
        }
        public static MediaItem MediaItemUpdateToDal(this MediaItemUpdate mirf)
        {
            return new MediaItem
            {
                MediaItem_Id = mirf.Media_Id,
                MediaType = mirf.MediaType,
                UrlItem = mirf.UrlItem,
                Description = mirf.Description,
            };
        }
        public static NEvenement NEvenementToDal(this NEvenementRegisterForm erf)
        {
            return new NEvenement
            {
                NEvenementDate = erf.NEvenementDate,
                NEvenementName = erf.NEvenementName,
                NEvenementDescription = erf.NEvenementDecription,
                PosLat = erf.PosLat,
                PosLong = erf.PosLong,
                Positif = erf.Positif,
                Organisateur_Id = erf.Organisateur_Id,
                NIcon_Id = erf.NIcon_Id,
                Recompense_Id = erf.Recompense_Id,
                Bonus_Id = erf.Bonus_Id,
                MediaItem_Id = erf.MediaItem_Id,
            };
        }
        public static NEvenement NEvenementUpdateToDal(this NEvenementUpdate erf)
        {
            return new NEvenement
            {
                NEvenement_Id = erf.NEvenement_Id,
                NEvenementDate = erf.NEvenementDate,
                NEvenementName = erf.NEvenementName,
                NEvenementDescription = erf.NEvenementDescription,
                PosLat = erf.PosLat,
                PosLong = erf.PosLong,
                Positif = erf.Positif,
                Organisateur_Id = erf.Organisateur_Id,
                NIcon_Id = erf.NIcon_Id,
                Recompense_Id = erf.Recompense_Id,
                Bonus_Id = erf.Bonus_Id,
                MediaItem_Id = erf.MediaItem_Id,
            };
        }
        public static NIcon NIconToDal(this NIconRegisterForm nIcon)
        {
            return new NIcon
            {
                NIconName = nIcon.NIconName,
                NIconUrl = nIcon.NIconUrl,
                NIconDescription = nIcon.NIconDescription,
            };
        }
        public static NIcon NIconUpdateToDal(this NIconUpdate nIcon)
        {
            return new NIcon
            {
                NIcon_Id = nIcon.NIcon_Id,
                NIconName = nIcon.NIconName,
                NIconUrl = nIcon.NIconUrl,
                NIconDescription = nIcon.NIconDescription,
            };
        }
        public static NPerson NPersonToDal(this NPersonRegisterForm nPerson)
        {
            return new NPerson
            {
                Lastname = nPerson.Lastname,
                Firstname = nPerson.Firstname,
                Email = nPerson.Email,
                Address_Street = nPerson.Address_Street,
                Address_Nbr = nPerson.Address_Nbr,
                PostalCode = nPerson.PostalCode,
                Address_City = nPerson.Address_City,
                Address_Country = nPerson.Address_Country,
                Telephone = nPerson.Telephone,
                Gsm = nPerson.Gsm,
            };
        }
        public static NPerson NPersonUpdateToDal(this NPersonUpdate nPerson)
        {
            return new NPerson
            {
                NPerson_Id = nPerson.NPerson_Id,
                Lastname = nPerson.Lastname,
                Firstname = nPerson.Firstname,
                Email = nPerson.Email,
                Address_Street = nPerson.Address_Street,
                Address_Nbr = nPerson.Address_Nbr,
                PostalCode = nPerson.PostalCode,
                Address_City = nPerson.Address_City,
                Address_Country = nPerson.Address_Country,
                Telephone = nPerson.Telephone,
                Gsm = nPerson.Gsm,
            };
        }
        public static NUser NUserToDal(this NUserRegisterForm nser)
        {
            return new NUser
            {
                Email = nser.Email,
                Pwd = nser.Pwd,
                NPerson_Id = nser.NPerson_Id,
                Role_Id = nser.Role_Id,
                Avatar_Id = nser.Avatar_Id,
                Point = nser.Point,
            };
        }

        public static NUser NUserUpdateToDal(this NUserUpdate nser)
        {
            return new NUser
            {
                NUser_Id = nser.NUser_Id,
                Email = nser.Email,
                Pwd = nser.Pwd,
                NPerson_Id = nser.NPerson_Id,
                Role_Id = nser.Role_Id,
                Avatar_Id = nser.Avatar_Id,
                Point = nser.Point,
            };
        }
        public static NVote NVoteToDal(this NVoteRegisterForm vrf)
        {
            return new NVote
            {
                NEvenement_Id = vrf.NEvenement_Id,
                FunOrNot = vrf.FunOrNot,
                Comment = vrf.Comment,
            };
        }
        public static Organisateur OrganisateurToDal(this OrganisateurRegisterForm orf)
        {
            return new Organisateur
            {
                CompanyName = orf.CompanyName,
                BusinessNumber = orf.BusinessNumber,
                NUser_Id = orf.NUser_Id,
                Point = orf.Point,
            };
        }

        public static Organisateur OrganisateurUpdateToDal(this OrganisateurUpdate orf)
        {
            return new Organisateur
            {
                Organisateur_Id = orf.Organisateur_Id,
                CompanyName = orf.CompanyName,
                BusinessNumber = orf.BusinessNumber,
                NUser_Id = orf.NUser_Id,
                Point = orf.Point,
            };
        }
        public static Recompense RecompenseToDal(this RecompenseRegisterForm rrf)
        {
            return new Recompense
            {
                Definition = rrf.Definition,
                Point = rrf.Point,
                Implication = rrf.Implication,
                Granted = rrf.Granted,
            };
        }

        public static Recompense RecompenseUpdateToDal(this RecompenseUpdate rrf)
        {
            return new Recompense
            {
                Recompense_Id = rrf.Recompense_Id,
                Definition = rrf.Definition,
                Point = rrf.Point,
                Implication = rrf.Implication,
                Granted = rrf.Granted,
            };
        }
        public static WeatherForecast WeatherForecastToDal(this WeatherForecastRegisterForm forecast)
        {
            return new WeatherForecast
            {
                Date = forecast.Date,
                TemperatureC = forecast.TemperatureC,
                TemperatureF = forecast.TemperatureF,
                Summary = forecast.Summary,
                Description = forecast.Description,
                Humidity = forecast.Humidity,
                Precipitation = forecast.Precipitation,
                NEvenement_Id = forecast.NEvenement_Id,
            };
        }
        public static WeatherForecast WeatherForecastUpdateToDal(this WeatherForecastUpdate forecast)
        {
            return new WeatherForecast
            {
                WeatherForecast_Id = forecast.WeatherForecast_Id,
                Date = forecast.Date,
                TemperatureC = forecast.TemperatureC,
                TemperatureF = forecast.TemperatureF,
                Summary = forecast.Summary,
                Description = forecast.Description,
                Humidity = forecast.Humidity,
                Precipitation = forecast.Precipitation,
                NEvenement_Id = forecast.NEvenement_Id,
            };
        }
    }
}
