namespace Privatly.API.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Сохраняет ожидающие изменения
    /// </summary>
    /// <returns>Колличество элементов которые были добавлены, изменены, удалены</returns>
    Task<int> CommitAsync();
}