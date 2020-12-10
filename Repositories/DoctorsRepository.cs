﻿//using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHospitalManagement.Models;

namespace HospitalManagementSystem.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly HospitalContext db;

        public DoctorsRepository(HospitalContext db)
        {
            this.db = db;
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            doctor.Id = db.Doctors.Count();

            db.Doctors.Add(doctor);

            db.SaveChanges();

            return doctor;
        }


        public Doctor DeleteDoctor(int id)
        {
            var doctor = GetDoctor(id);
            if (doctor != null)
            {
                foreach (var patient in db.Patients.Where(p => p.DoctorId == id))
                {
                    patient.DoctorId = null;
                    patient.Doctor = null;
                }
                
                db.Doctors.Remove(doctor);
                db.SaveChanges();
            }
			return doctor;
        }

        public Doctor GetDoctor(int id)
        {
            return db.Doctors.Find(id);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return db.Doctors.AsNoTracking().ToList();
        }

        public IEnumerable<Patient> GetPatients(int id)
        {
            var doctor = GetDoctor(id);

            if (doctor == null)
                throw new EntityNotFoundException<Doctor>(id);

            return db.Patients.Where(p => p.DoctorId == id);
        }

        public Doctor UpdateDoctor(int id, Doctor updatedDoctor)
        {
            var doctor = GetDoctor(id);

            if (doctor == null)
                throw new EntityNotFoundException<Doctor>(id);

            doctor.Name = updatedDoctor.Name;
           
            db.SaveChanges();
			return updatedDoctor;

		}
        
    }
}