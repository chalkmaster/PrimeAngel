using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using PrimeAngel.Perceptions;
using PrimeAngel.Perceptions.PrimeBuilder;

namespace PrimeAngel.Sensors.PrimeBuilder.Business
{
    public class InstanceUseSensor : ISensor
    {

        IList<IPerception> ISensor.Percept()
        {
            var perceptions = new List<IPerception>
            {
                GetActiveInstanceWithoutUsePerception()
            };

            return perceptions;
        }

        private ActiveInstancesWithoutUsePerception GetActiveInstanceWithoutUsePerception()
        {
            var activeInstancesWithoutUse = new ActiveInstancesWithoutUsePerception();

                using (var cn = new SqlConnection())
                {
                    cn.ConnectionString =
                        @"Server=200.170.174.154\MSSQL2008R2;initial catalog=OSMobile;uid=sa;pwd=qwe123!@#;";
                    cn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = GetActiveInstanceWithoutUseCommandText();

                        using (var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                activeInstancesWithoutUse.Instances.Add(new ActiveInstancesWithoutUsePerception.InstanceData
                                {
                                    InstanceId = dr.GetInt16(0),
                                    InstanceName = dr.GetString(1),
                                    WorkingUsers = dr.GetInt32(2),
                                    CreatedTasks = dr.GetInt32(3),
                                    TotalActiveUsers = dr.GetInt32(4),
                                    TotalActiveWebUsers = dr.GetInt32(5),
                                    TotalActiveDeviceUsers = dr.GetInt32(6),
                                    LastUseDate = dr.GetDateTime(7)
                                });
                            }
                        }
                    }
                }

            return activeInstancesWithoutUse;
        }

        private static string GetActiveInstanceWithoutUseCommandText()
        {
            return @"SELECT C.Id as InstanceId, C.Name as InstanceName, 
	                                    ISNULL(EP.TotalUsuarioAtivo, 0) as WorkingUsers, 
	                                    ISNULL(TotalOSGeral, 0) as CreatedTasks,
	                                    ISNULL((Select  Count(1) From [Security].[User] U Where  u.Active = 1 and u.company = C.id),0) as TotalActiveUsers,
	                                    ISNULL((Select  Count(1) From [Security].[User] U Where  u.Active = 1 and u.company = C.id and WebAccess = 1),0) as TotalActiveWebUsers,
	                                    ISNULL((Select  Count(1) From [Security].[User] U Where  u.Active = 1 and u.company = C.id and DeviceAccess = 1),0) as TotalActiveDeviceUsers,
	                                    ISNULL((Select max(Data) from [INFRAESTRUTURA].[EstatisticaProdutividade] iEP Where idEmpresa = C.Id and iEP.TotalUsuarioAtivo > 0),0) as LastDay
                                    From [OSMobile].[INFRAESTRUTURA].[EstatisticaProdutividade] EP
	                                    Inner Join [OSMobile].[Foundation].[Company] C
		                                    On C.Id = EP.idEmpresa
	                                    Inner Join [OSMobile].[Foundation].[CompanyParameters] CP
		                                    On C.Id = CP.Company
	                                    Where C.Active = 1
		                                    And ISNULL(EP.TotalUsuarioAtivo, 0) = 0
		                                    And EP.Data = '2015-05-19'
		                                    And CP.CompanyCategory = 1 ";
        }

    }
}
