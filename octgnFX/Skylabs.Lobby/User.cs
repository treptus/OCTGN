﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Skylabs.Lobby
{
    [Serializable]
    public class User
    {
        [Serializable]
        public enum UserLevel { Standard = 0, Moderator = 1, Admin = 2 };

        public int UID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public UserLevel Level { get; set; }

        public bool IsComplete
        {
            get
            {
                //Isn't nesting fun!
                if(UID >= 0)
                    if(Email != null)
                        if(!String.IsNullOrWhiteSpace(Email))
                            if(Password != null)
                                if(!String.IsNullOrWhiteSpace(Password))
                                    if(DisplayName != null)
                                        if(!String.IsNullOrWhiteSpace(DisplayName))
                                            if(Level != null)
                                                return true;
                return false;
            }
        }

        public byte[] Serialize()
        {
            using(MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Flush();
                return ms.ToArray();
            }
        }

        public static User Deserialize(byte[] data)
        {
            using(MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (User)bf.Deserialize(ms);
            }
        }
    }
}