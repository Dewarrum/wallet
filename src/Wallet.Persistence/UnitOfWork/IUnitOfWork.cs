namespace Wallet.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}
