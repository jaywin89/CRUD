﻿using PersonalReferenceProject.Adapter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Service
{
    public class BaseService
    {
      public IDbAdapter Adapter
        {
            get
            {
                return new DbAdapter(new SqlCommand(), new SqlConnection("Server=sql7001.site4now.net;Database=DB_A2CD42_coderepo;User Id=DB_A2CD42_coderepo_admin;Password=qwerty1234"));
                //("Server=DESKTOP-J3U9NDT\\SQLEXPRESS;Database=QuickReference;Trusted_Connection=True"));
            }
        }
    }
}