using Context;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IServiceScope _scope;
        private readonly StudentContext _studentContext;

        public StudentRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _studentContext = _scope.ServiceProvider.GetRequiredService<StudentContext>();
        }

        public async Task<bool> Create(Student student)
        {
            student.Enabled = true;
            student.LastUpdated = DateTime.Now.ToString();

            _studentContext.Students.Add(student);

            var result = await _studentContext.SaveChangesAsync();

            if (result == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> Update(Student student)
        {
            var result = Get(student.Id);
            try
            {
                if (result != null)
                {
                    result.FirstName = student.FirstName;
                    result.LastName = student.LastName;
                    result.Dob = student.Dob;
                    result.Sex = student.Sex;
                    result.Enabled = true;
                    result.LastUpdated = DateTime.Now.ToString();

                    var resultUpdate = await _studentContext.SaveChangesAsync();

                    if (resultUpdate == 1)
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

            return false;

        }

        public Student Get(int id)
        {
            var result = _studentContext.Students.Where(x => x.Id == id).FirstOrDefault();

            return result;
            
        }

        public List<Student> GetAll()
        {
            return _studentContext.Students.Where(x => x.Enabled == true).ToList();

        }

        public async Task<bool> Delete(int id)
        {
            var result = Get(id);
            try
            {
                if (result != null)
                {
                    result.Enabled = false;
                    result.LastUpdated = DateTime.Now.ToString();

                    var resultUpdate = await _studentContext.SaveChangesAsync();

                    if (resultUpdate == 1)
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return false;
        }
    }
}
