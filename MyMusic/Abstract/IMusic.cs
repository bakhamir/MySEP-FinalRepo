using MyMusic.Models;

namespace MyMusic.Abstract
{
    public interface IMusic<T> where T : class
    {
        IEnumerable<T> GetValues();
        bool AddSong (T song);
        bool DeleteSong(int id);
        bool EditSong(song song);
        object GetSongById(int id);
    }
}
