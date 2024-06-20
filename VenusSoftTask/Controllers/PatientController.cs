using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenusSoftTask.Context;
using VenusSoftTask.DTOs;
using VenusSoftTask.Models;

namespace VenusSoftTask.Controllers;

[Route("API/patient")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly PatientDBContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<PatientController> _logger;

    public PatientController(PatientDBContext dbContext, IMapper mapper,ILogger<PatientController> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddPatient([FromBody] PatientCreationDTO patientCreationDTO)
    {
        //empty request
        if (patientCreationDTO == null) return BadRequest("Invalid patient data.");

        //check duplicate entry
        var isDuplicate = _dbContext.Patients
            .Include(p => p.PatientEmails)
            .Include(p => p.PatientPhoneNumbers)
            .Any(p => p.FirstName == patientCreationDTO.FirstName &&
                      p.LastName == patientCreationDTO.LastName &&
                      p.PatientEmails.Any(e => e.Email == patientCreationDTO.Email));
        if (isDuplicate) return Conflict("Patient already exists.");

        //add Patient
        var patient = _mapper.Map<Patient>(patientCreationDTO);
        _dbContext.Add((object)patient);

        //save changes to db
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //Log Error
            _logger.LogError(ex, "An error occurred while saving the patient");
            return StatusCode(500, "Something went wrong while saving patient");
        }

        //add PatientEmail
        var patientEmail = new PatientEmail
        {
            PatientId = patient.Id,
            Email = patientCreationDTO.Email
        };
        _dbContext.Add(patientEmail);

        //add PatientNumber
        var patientNumber = new PatientPhoneNumber
        {
            PatientId = patient.Id,
            Phone = patientCreationDTO.Phone
        };
        _dbContext.Add(patientNumber);

        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //remove created patient
            _dbContext.Remove(patient);

            //Log Error
            _logger.LogError(ex, "An error occurred while saving the patient");
            return StatusCode(500, "Something went wrong while saving patient");
        }

        return Ok("Patient added to records successfully!");
    }

    [HttpGet(Name = "getPatients")]
    public IActionResult GetPatients(int? id)
    {
        if (id == null)
        {
            //return all patients
            var patients = _dbContext.Patients
                .Include(p => p.PatientEmails)
                .Include(p => p.PatientPhoneNumbers)
                .ToList();
            var patientsDTO = _mapper.Map<List<PatientDTO>>(patients);
            return Ok(patientsDTO);
        }

        //find patient
        var patient = _dbContext.Patients
            .Include(p => p.PatientEmails)
            .Include(p => p.PatientPhoneNumbers)
            .FirstOrDefault(x => x.Id == id);

        if (patient == null)
            //patient does not exist
            return NotFound($"Patient with id {id} does not exist.");

        //return patient
        var patientDTO = _mapper.Map<PatientDTO>(patient);
        return Ok(patientDTO);
    }

    [HttpPut]
    public IActionResult UpdatePatient( PatientUpdationDTO patientUpdationDTO)
    {
        //check Duplicate 
        var isDuplicate = _dbContext.Patients
            .Include(p => p.PatientEmails)
            .Include(p => p.PatientPhoneNumbers)
            .Any(p => p.Id != patientUpdationDTO.Id && p.FirstName == patientUpdationDTO.FirstName &&
                      p.LastName == patientUpdationDTO.LastName &&
                      p.PatientEmails.Any(e => e.Email == patientUpdationDTO.Email));
        if (isDuplicate) return Conflict("Patient already exists.");

        var patientDb = _dbContext.Patients
            .Include(p => p.PatientEmails)
            .Include(p => p.PatientPhoneNumbers).FirstOrDefault(p => p.Id == patientUpdationDTO.Id);
        if (patientDb == null) return NotFound("Patient does not exist.");

        _mapper.Map(patientUpdationDTO, patientDb);
        var emailEntity = patientDb.PatientEmails.FirstOrDefault();
        if (emailEntity != null)
        {
            emailEntity.Email = patientUpdationDTO.Email;
        }
        else
        {
            patientDb.PatientEmails.Add(new PatientEmail { Email = patientUpdationDTO.Email });
        }

        var phoneEntity = patientDb.PatientPhoneNumbers.FirstOrDefault();
        if (phoneEntity != null)
        {
            phoneEntity.Phone = patientUpdationDTO.Phone;
        }
        else
        {
            patientDb.PatientPhoneNumbers.Add(new PatientPhoneNumber { Phone = patientUpdationDTO.Phone });
        }

        try
        {
             _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the patient");
            return StatusCode(500, "Something went wrong while updating patient");
        }
        
        //save changes to db
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //Log Error
            _logger.LogError(ex, "An error occurred while updating the patient");
            return StatusCode(500, "Something went wrong while updating patient");
        }

        return Ok("Patient updated successfully!");
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePatient(int id)
    {
        var patient =  _dbContext.Patients.FirstOrDefault(p => p.Id == id);
        if (patient == null)
                return NotFound();
            


        _dbContext.Remove(patient);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //Log Error
            _logger.LogError(ex, "An error occurred while deleting the patient");
            return StatusCode(500, "Something went wrong while deleting patient");
        }
        return Ok(new { message ="Record deleted Successfully!"});
    }
    
}