using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace SecretBlend
{
    public class Algorithm
    {
        // общий для всех потоков/таймеров объект
        private static object fileLock = new object();

        public static void Hide(string wavfile, string keyword, string message)
        {
            try
            {
                Stopwatch time = new Stopwatch();
                time.Start();

                // ЧТЕНИЕ WAV АУДИОФАЙЛА
                using (var song = new NAudio.Wave.WaveFileReader(wavfile))
                {
                    // cоздаем новый массив байтов для хранения содержимого аудиофайла
                    var frame_bytes = new byte[song.Length];
                    // cчитываем содержимое аудиофайла в массив байтов, начиная с индекса 0
                    song.Read(frame_bytes, 0, frame_bytes.Length);

                    // проверка, может ли сообщение быть записанным в файл
                    if ((message.Length + 3) * 8 > frame_bytes.Length)
                    {
                        throw new ArgumentException();
                    }

                    // добавление фиктивных данных для заполнения оставшихся байтов
                    message += new string('#', 3);

                    // КОНВЕРТИРОВАНИЕ СООБЩЕНИЯ В БИТОВЫЙ МАССИВ
                    // конкатенация каждого символа сообщения как двоичной строки, заполненную 0 слева, чтобы получилось 8 цифр
                    var bits_string = string.Concat(Encoding.UTF8.GetBytes(message).Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits = bits_string.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    // КОНВЕРТИРОВАНИЕ КЛЮЧА В БИТОВЫЙ МАССИВ
                    // конкатенация каждого символа сообщения как двоичной строки, заполненную 0 слева, чтобы получилось 8 цифр
                    var bits_string_key_word = string.Concat(Encoding.UTF8.GetBytes(keyword).Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits_key_word = bits_string_key_word.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    // АЛГОРИТМ ЗАШИФРОВАНИЯ
                    // заменяем биты в байтах аудиофайла на биты сообщения в зависимости от битов ключа
                    int n = 0, i = 0, j = 0;
                    while (true)
                    {
                        // если все биты сообщения записаны, цикл заканчивается
                        if (j > bits.Count - 1)
                        {
                            break;
                        }
                        // итерация по битам сообщения
                        var bit = bits[j];
                        j++;
                        // если все биты ключевого слова пройдены, а сообщение еще записывается, начинаем итерирование по битам ключа сначала
                        if (n > bits_key_word.Count - 1)
                        {
                            n = 0;
                        }
                        // в зависимости от значения бита ключа выбираем позицию модифицируемого байта
                        i += bits_key_word[n];
                        // на выбранной позиции модифицируем байт: заменяем последний бит битом шифруемого сообщения
                        frame_bytes[i] = (byte)((frame_bytes[i] & 254) | bit);
                        i++;
                        n++;
                    }

                    // получение новых модифицированных байтов
                    var frame_modified = frame_bytes;

                    // формирование названия нового файла
                    string outputFile = wavfile.Replace(".wav", "_hidden.wav");

                    // запись пути сохранения нового аудиофайла в глобальную переменную
                    GlobalClass.EncryptedWAVFile = outputFile;

                    // ЗАПИСЬ ПОЛУЧЕННЫХ БАЙТОВ В НОВЫЙ WAV АУДИО ФАЙЛ
                    using (var writer = new NAudio.Wave.WaveFileWriter(outputFile, song.WaveFormat))
                    {
                        writer.Write(frame_modified, 0, frame_modified.Length);
                    }
                }

                time.Stop();
                GlobalClass.Time = time.ElapsedMilliseconds.ToString();
            }
            catch (IOException)
            {
                // ЕСЛИ ИСХОДНЫЙ WAV АУДИО ФАЙЛ НЕ НАЙДЕН
                MessageBox.Show($"Невозможно открыть WAV-файл.\nУказанное расположение: {GlobalClass.WAVfile}.\nНайдите и выберите существующий файл.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException)
            {
                // ЕСЛИ ИСХОДНЫЙ WAV АУДИО ФАЙЛ НЕ НАЙДЕН
                MessageBox.Show($"Аудиофайл не достаточно большого размера, чтобы зашифровать ваше сообщение. Измените WAV-файл или сообщение.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Extract(string hidden_file, string keyword)
        {
            try
            {
                Stopwatch time = new Stopwatch();
                time.Start();

                // ЧТЕНИЕ WAV АУДИОФАЙЛА
                using (var song = new NAudio.Wave.WaveFileReader(hidden_file))
                {
                    // cоздаем новый массив байтов для хранения содержимого аудиофайла
                    var frame_bytes = new byte[song.Length];
                    // cчитываем содержимое аудиофайла в массив байтов, начиная с индекса 0
                    song.Read(frame_bytes, 0, frame_bytes.Length);

                    // КОНВЕРТИРОВАНИЕ КЛЮЧА В БИТОВЫЙ МАССИВ
                    // конкатенация каждого символа сообщения как двоичной строки, заполненную 0 слева, чтобы получилось 8 цифр

                    var bits_string_key_word = string.Concat(Encoding.UTF8.GetBytes(keyword).Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits_key_word = bits_string_key_word.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    var extracted = new List<int>();
                    var n = 0;
                    var i = 0;
                    var o = 0;

                    var bitstring = "";

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while (true)
                    {
                        if (n > bits_key_word.Count - 1)
                            n = 0;
                        i = i + bits_key_word[n];
                        if (i > frame_bytes.Length - 1)
                            break;
                        extracted.Add(frame_bytes[i] & 1);
                        i++;
                        n++;

                        if (extracted.Count == 8)
                        {
                            string bits_string = $"{extracted[0]}{extracted[1]}{extracted[2]}{extracted[3]}{extracted[4]}{extracted[5]}{extracted[6]}{extracted[7]}";
                            bitstring += bits_string;

                            if (bits_string.Equals("00100011"))
                            {
                                o++;
                                if (o == 3) break;
                            }
                            else
                            {
                                o = 0;
                            }
                            extracted.Clear();
                        }

                        if (stopwatch.Elapsed.TotalSeconds >= 10)
                        {
                            stopwatch.Stop();
                            break;
                        }
                    }

                    if (stopwatch.IsRunning)
                    {
                        stopwatch.Stop();
                    }

                    // если ключ неправильный или не найдено "###"

                    byte[] bytes = Enumerable.Range(0, bitstring.Length / 8).Select(b => Convert.ToByte(bitstring.Substring(b * 8, 8), 2)).ToArray();

                    // кодирование байтового массива как UTF-8
                    string utf8String = Encoding.UTF8.GetString(bytes);
                    string result = "";

                    if (o == 3)
                    {
                        result = utf8String.TrimEnd('#');
                    }
                    else
                    {
                        result = utf8String;
                    }

                    // формирование названия нового файла
                    string outputFile = hidden_file.Replace(".wav", "_extracted.txt");

                    // запись пути сохранения нового аудиофайла в глобальную переменную
                    GlobalClass.ExtractFileResult = outputFile;

                    if (o == 3)
                    {
                        using (var stream = new StreamWriter(outputFile, false, Encoding.UTF8))
                        {
                            stream.Write(result);
                        }
                    }

                    else
                    {
                        using (var stream = new StreamWriter(outputFile))
                        {
                            stream.Write(result);
                        }
                    }
                }

                time.Stop();
                GlobalClass.Time = time.ElapsedMilliseconds.ToString();
            }
            catch (IOException)
            {
                // ЕСЛИ ИСХОДНЫЙ WAV АУДИО ФАЙЛ НЕ НАЙДЕН
                MessageBox.Show($"Невозможно открыть WAV-файл.\nУказанное расположение: {GlobalClass.WAVfile}.\nНайдите и выберите существующий файл.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}