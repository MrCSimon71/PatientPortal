using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PDDS.PatientData.Core.Entities;

namespace PDDS.PatientData.Core.Repositories
{
    public interface IPatientRepository<Patient, TId> : IBaseRepository<Patient, TId>
    {
    }
}
