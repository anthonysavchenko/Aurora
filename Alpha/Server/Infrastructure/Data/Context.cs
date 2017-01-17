using System.Data.Entity;
using Taumis.Alpha.Server.Core.Model.RefBook;
using Taumis.Cointreau.Server.Infrastructure.Data.Configurations;

namespace Taumis.Cointreau.Server.Infrastructure.Data
{
    /// <summary>
    /// Класс доступа к данным в БД
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Context()
            : base("AlphaDataBaseConnectionString")
        {
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        #region Overrides of DbContext

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        #endregion
    }
}