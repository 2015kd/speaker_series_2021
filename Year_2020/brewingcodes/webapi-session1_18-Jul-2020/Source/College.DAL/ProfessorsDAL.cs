﻿using College.Core.Entities;
using College.Core.Interfaces;
using College.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.DAL
{

    public class ProfessorsDAL : IProfessorsDAL
    {
        private readonly CollegeDbContext _collegeDbContext;
        private readonly ILogger<ProfessorsDAL> _logger;

        public ProfessorsDAL(CollegeDbContext collegeDbContext, ILogger<ProfessorsDAL> logger)
        {
            _collegeDbContext = collegeDbContext;

            _logger = logger;
        }

        public async Task<IEnumerable<Professor>> GetAllProfessors()
        {
            _logger.Log(LogLevel.Debug, "Request Received for ProfessorDAL::GetAllProfessors");

            var professors = await _collegeDbContext.Professors.ToListAsync();

            _logger.Log(LogLevel.Debug, "Returning the results from ProfessorDAL::GetAllProfessors");

            return professors;
        }

        public async Task<Professor> GetProfessorById(Guid professorId)
        {
            Professor professor = null;

            _logger.Log(LogLevel.Debug, "Request Received for ProfessorDAL::GetProfessorById");

            professor = await _collegeDbContext.Professors
                .Where(record => record.ProfessorId == professorId)
                .Include(student => student.Students)
                .FirstOrDefaultAsync();

            _logger.Log(LogLevel.Debug, "Returning the results from ProfessorDAL::GetProfessorById");

            return professor;
        }
    }


}
