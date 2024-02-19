namespace MyMusic.Abstract
{
    public interface IMusic<T> where T : class
    {
        IEnumerable<T> GetValues();
        bool AddSong (T song);
    }
}
