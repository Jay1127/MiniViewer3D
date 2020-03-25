namespace MiniViewer3D.Models
{
    /// <summary>
    /// Transaction 인터페이스
    /// </summary>
    public interface ITransactionUnit
    {
        /// <summary>
        /// 원래 상태로 되돌림.
        /// </summary>
        void Rollback();

        /// <summary>
        /// 변경 사항을 저장함.
        /// </summary>
        void SaveChanges();
    }
}