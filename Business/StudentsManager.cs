using Business.Interfaces;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class StudentsManager : IStudentsManager
    {
        private IStudentRepository _repository;
        public StudentsManager(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Student> Create(Student student)
        {
            var result = await _repository.Create(student);

            if (result)
                return student;
            else
                return null;    
        }

        public Student Get(int id)
        {
            return _repository.Get(id);
        }

        public async Task<Student> Update(Student student)
        {
            var result = await _repository.Update(student);

            if (result)
                return student;
            else
                return null;
        }

        public void LoadTestData()
        {
            var result = _repository.Get(1);
            if (result == null)
            { 
                Student student = new Student
                {
                    Id = 1,
                    FirstName = "Charlie",
                    LastName = "Freak",
                    Sex = "Male",
                    Dob = "03-25-1981",
                    LastUpdated = DateTime.Now.ToString(),
                    Enabled = true
                };
                _repository.Create(student);
                student = new Student
                {
                    Id = 2,
                    FirstName = "Janie",
                    LastName = "Runaway",
                    Sex = "Female",
                    Dob = "20-09-1989",
                    LastUpdated = DateTime.Now.ToString(),
                    Enabled = true
                };
                _repository.Create(student);
            }
        }

        public List<Student> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result;
        }
    }
}
