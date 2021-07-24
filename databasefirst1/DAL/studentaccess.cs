using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using databasefirst1.Models;
using databasefirst1.DAL;

namespace databasefirst1.DAL
{
    public class studentaccess : DbContext
    {


        public IEnumerable<student> Students(int procid, student Model)
        {
            var param = new SqlParameter[]
                {
                new SqlParameter{ParameterName = "@StudentId",Value=Model.StudentId==null?0:Model.StudentId},
                new SqlParameter{ParameterName = "@Name",Value=Model.Name??string.Empty},
                new SqlParameter{ParameterName = "@Gender",Value=Model.Gender??string.Empty},
                 new SqlParameter{ParameterName = "@Age",Value=Model.Age==null?0:Model.Age},
                new SqlParameter{ParameterName = "@MobileNo",Value=Model.MobileNo??string.Empty},
                new SqlParameter{ParameterName = "@Email",Value=Model.Email??string.Empty},
                new SqlParameter{ParameterName = "@imagee",Value=Model.imagee??string.Empty},
                new SqlParameter{ParameterName = "@address",Value=Model.address??string.Empty},
               
                 new SqlParameter{ParameterName = "@ProcId",Value=procid},
                };
            var sqlquery = @"proc_addstudent @StudentId,@Name,@Gender,@Age,@MobileNo,@Email,@imagee,@address,@ProcId";
            return this.Database.SqlQuery<student>(sqlquery, param);
        }

        //public System.Data.Entity.DbSet<databasefirst1.Models.student> students { get; set; }
    }
}
