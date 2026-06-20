using DCOps.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DCOps.Web.Data;

public static class RuntimeSchema
{
    public static async Task EnsureV9SchemaAsync(AppDbContext db)
    {
        // Ensure schema is up-to-date; EnsureCreated handles base schema.
        // Any additional migrations or schema alterations can be added here.
        await Task.CompletedTask;
    }
}

public static class RuntimeSeed
{
    public static async Task SeedCloudAndVmwareAsync(AppDbContext db)
    {
        // Seed cloud and VMware connector definitions if needed
        if (!db.AutomationConnectors.Any())
        {
            db.AutomationConnectors.AddRange(
                new AutomationConnector
                {
                    ConnectorCode = "VMWARE_VC",
                    Name = "VMware vCenter",
                    ConnectorType = "vmware",
                    BaseUrl = "https://vcenter.local",
                    AuthType = "basic",
                    Status = "active",
                    CreatedAt = DateTime.UtcNow
                },
                new AutomationConnector
                {
                    ConnectorCode = "AWS_EC2",
                    Name = "AWS EC2",
                    ConnectorType = "cloud_aws",
                    BaseUrl = "https://ec2.amazonaws.com",
                    AuthType = "api_key",
                    Status = "active",
                    CreatedAt = DateTime.UtcNow
                }
            );
            await db.SaveChangesAsync();
        }
    }

    public static async Task SeedV9EnterpriseAsync(AppDbContext db)
    {
        // Seed enterprise SLA definitions if none exist
        if (!db.SlaDefinitions.Any())
        {
            db.SlaDefinitions.AddRange(
                new SlaDefinition
                {
                    SlaCode = "SLA-P1",
                    Name = "حرج - الأولوية الأولى",
                    ResponseTimeHours = 1,
                    ResolutionTimeHours = 4,
                    Priority = "critical",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new SlaDefinition
                {
                    SlaCode = "SLA-P2",
                    Name = "عالي - الأولوية الثانية",
                    ResponseTimeHours = 4,
                    ResolutionTimeHours = 8,
                    Priority = "high",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new SlaDefinition
                {
                    SlaCode = "SLA-P3",
                    Name = "متوسط - الأولوية الثالثة",
                    ResponseTimeHours = 8,
                    ResolutionTimeHours = 24,
                    Priority = "medium",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new SlaDefinition
                {
                    SlaCode = "SLA-P4",
                    Name = "منخفض - الأولوية الرابعة",
                    ResponseTimeHours = 24,
                    ResolutionTimeHours = 72,
                    Priority = "low",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
            await db.SaveChangesAsync();
        }
    }

    public static async Task SeedV10DemoEnterpriseDataAsync(AppDbContext db)
    {
        // Seed one admin user if no users exist
        if (!db.Users.Any())
        {
            db.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Email = "admin@dcops.local",
                FullName = "مدير النظام",
                Role = "admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }
    }
}
