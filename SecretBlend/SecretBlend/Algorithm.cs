using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SecretBlend
{
    public class Algorithm
    {
        public static void Hide(string wavfile, string keyword, string message)
        {
            // region Read wave audio file
            try
            {
                using (var song = new NAudio.Wave.WaveFileReader(wavfile))
                {
                    // endregion

                    // region Read frames and convert to byte array
                    var frame_bytes = new byte[song.Length];
                    song.Read(frame_bytes, 0, frame_bytes.Length);
                    // endregion

                    // region Check that message fits in file
                    if ((message.Length + 3) * 8 > frame_bytes.Length)
                    {
                        Console.WriteLine("Audio file is not large enough");
                        Environment.Exit(1);
                    }
                    // endregion

                    // region Append dummy data to fill out rest of the bytes. Receiver shall detect and remove these characters
                    message += new string('#', 3);
                    // endregion

                    // region Convert text to bit array
                    var bits_string = string.Concat(message.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
                    var bits = bits_string.Select(c => Convert.ToInt32(c.ToString())).ToList();
                    // endregion

                    // region Convert key word to bit array
                    var bits_string_key_word = string.Concat(keyword.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
                    var bits_key_word = bits_string_key_word.Select(c => Convert.ToInt32(c.ToString())).ToList();
                    // endregion

                    // region Replace LSB of each byte of the audio data by one bit from the text bit array
                    int n = 0, i = 0, j = 0;
                    while (true)
                    {
                        if (j > bits.Count - 1)
                        {
                            break;
                        }
                        var bit = bits[j];
                        j++;
                        if (n > bits_key_word.Count - 1)
                        {
                            n = 0;
                        }
                        i += bits_key_word[n];
                        frame_bytes[i] = (byte)((frame_bytes[i] & 254) | bit);
                        i++;
                        // If n is higher than the length of the key, start from the beginning of the key
                        n++;
                    }
                    // endregion

                    // region Get the modified bytes
                    var frame_modified = frame_bytes;
                    // endregion

                    string outputFile = wavfile.Replace(".wav", "_hidden.wav");

                    GlobalClass.EncryptedWAVFile = outputFile;

                    // region Write bytes to a new wave audio file
                    using (var writer = new NAudio.Wave.WaveFileWriter(outputFile, song.WaveFormat))
                    {
                        writer.Write(frame_modified, 0, frame_modified.Length);
                    }
                    // endregion
                }
            }
            catch (Exception)
            {
                // If such file cannot be found it will be reported
                Console.WriteLine("Cannot open audiofile with path " + wavfile);
                Environment.Exit(1);
            }
            // endregion
        }
    }
}
