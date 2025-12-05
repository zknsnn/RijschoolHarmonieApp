using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly RijschoolHarmonieAppContext dbHarmonie;

    public AppointmentRepository(RijschoolHarmonieAppContext dbHarmonie)
    {
        this.dbHarmonie = dbHarmonie;
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        dbHarmonie.Appointments.Add(appointment);
        await dbHarmonie.SaveChangesAsync();
        return appointment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await dbHarmonie.Appointments.FindAsync(id);
        if (appointment == null)
            return false;

        dbHarmonie.Appointments.Remove(appointment);
        await dbHarmonie.SaveChangesAsync();
        return true;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await dbHarmonie
            .Appointments.Include(a => a.Instructor)
            .Include(a => a.Student)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int appointmentId)
    {
        return await dbHarmonie
            .Appointments.Include(a => a.Instructor)
            .Include(a => a.Student)
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public async Task<List<Appointment>> GetByInstructorAsync(
        int instructorId,
        DateTime? start = null,
        DateTime? end = null
    )
    {
        var query = dbHarmonie
            .Appointments.Include(a => a.Instructor)
            .Include(a => a.Student)
            .Where(a => a.InstructorId == instructorId);

        if (start.HasValue)
            query = query.Where(a => a.StartTime >= start.Value);

        if (end.HasValue)
            query = query.Where(a => a.EndTime <= end.Value);

        return await query.OrderBy(a => a.StartTime).ToListAsync();
    }

    public async Task<List<Appointment>> GetByStudentAsync(
        int studentId,
        DateTime? start = null,
        DateTime? end = null
    )
    {
        var query = dbHarmonie
            .Appointments.Include(a => a.Instructor)
            .Include(a => a.Student)
            .Where(a => a.StudentId == studentId);

        if (start.HasValue)
            query = query.Where(a => a.StartTime >= start.Value);

        if (end.HasValue)
            query = query.Where(a => a.EndTime <= end.Value);

        return await query.OrderBy(a => a.StartTime).ToListAsync();
    }

    public async Task<List<Appointment>> GetFilteredAsync(
        int? instructorId,
        int? studentId,
        DateTime? start,
        DateTime? end,
        AppointmentType? type
    )
    {
        var query = dbHarmonie
            .Appointments.Include(a => a.Instructor)
            .Include(a => a.Student)
            .AsQueryable();

        if (instructorId.HasValue)
            query = query.Where(a => a.InstructorId == instructorId.Value);

        if (studentId.HasValue)
            query = query.Where(a => a.StudentId == studentId.Value);

        if (start.HasValue)
            query = query.Where(a => a.StartTime >= start.Value);

        if (end.HasValue)
            query = query.Where(a => a.EndTime <= end.Value);

        if (type.HasValue)
            query = query.Where(a => a.Type == type.Value);

        return await query.OrderBy(a => a.StartTime).ToListAsync();
    }

    public async Task<Appointment> UpdateAsync(Appointment appointment)
    {
        dbHarmonie.Appointments.Update(appointment);
        await dbHarmonie.SaveChangesAsync();
        return appointment;
    }
}
