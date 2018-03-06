using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karaoke
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SoftUniKaraoke
    {
        class Songs
        {
            public string Participant { get; set; }
            public string Song { get; set; }
            public string Ward { get; set; }
            public int NumberOfWards { get; set; }
            public List<string> SongList { get; set; }
            public List<string> AwardsList { get; set; }

        }
        class Calculation
        {
            public void Cal()
            {
                Dictionary<string, Songs> song = new Dictionary<string, Songs>();
                string[] part = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                string[] availableSongs = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                while (true)
                {
                    string command = Console.ReadLine();
                    if (command == "dawn") break;
                    string[] comm = command.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    string partname = comm[0];
                    string songName = comm[1];
                    string award = comm[2];
                    Songs op = new Songs();
                    op.Participant = partname;
                    op.Song = songName;
                    op.Ward = award;
                    op.SongList = new List<string>();
                    op.AwardsList = new List<string>();
                    if (part.Contains(op.Participant))
                    {
                        if (!song.ContainsKey(op.Participant))
                        {
                            if (availableSongs.Contains(op.Song))
                            {
                                op.NumberOfWards += 1;
                                op.SongList.Add(op.Song);
                                op.AwardsList.Add(op.Ward);

                                song.Add(op.Participant, op);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < song.Count; i++)
                            {
                                if (song.ContainsKey(op.Participant))
                                {
                                    if (availableSongs.Contains(op.Song))
                                    {
                                        KeyValuePair<string, Songs> songPath = song.ElementAt(i);
                                        if (songPath.Key == op.Participant)
                                        {
                                            if (!songPath.Value.SongList.Contains(op.Song))
                                            {
                                                songPath.Value.SongList.Add(op.Song);
                                                if (!songPath.Value.AwardsList.Contains(op.Ward))
                                                {
                                                    songPath.Value.AwardsList.Add(op.Ward);
                                                    songPath.Value.NumberOfWards += 1;

                                                }
                                            }
                                            else if (songPath.Value.SongList.Contains(op.Song))
                                            {
                                                if (!songPath.Value.AwardsList.Contains(op.Ward))
                                                {
                                                    songPath.Value.AwardsList.Add(op.Ward);
                                                    songPath.Value.NumberOfWards += 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                if (song.Count > 0)
                {
                    foreach (var p in song.OrderByDescending(r => r.Value.NumberOfWards).ThenBy(r => r.Key))
                    {


                        Console.WriteLine($"{p.Key}: {p.Value.NumberOfWards} awards");
                        p.Value.AwardsList.Sort();
                        foreach (var k in p.Value.AwardsList)
                        {
                            Console.WriteLine($"--{k}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No awards");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Calculation cal = new Calculation();
                cal.Cal();

            }
        }
    }
}
