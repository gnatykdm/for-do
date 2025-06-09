using ForDoListApp.Data;

namespace ForDoListApp.Data {
    public static class DbSeeder
    {
        public static void SeedAll(AppDbContext context)
        {
            UserSeeder.Seed(context);
            TaskSeeder.Seed(context);
        }
    }
}