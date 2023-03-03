using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

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
                    var bits_string = string.Concat(message.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits = bits_string.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    // КОНВЕРТИРОВАНИЕ КЛЮЧА В БИТОВЫЙ МАССИВ
                    // конкатенация каждого символа сообщения как двоичной строки, заполненную 0 слева, чтобы получилось 8 цифр
                    var bits_string_key_word = string.Concat(keyword.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits_key_word = bits_string_key_word.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    // АЛГОРИТМ ЗАШИФРОВАНИЯ
                    // заменяем биты в байттах аудиофайла на биты сообщения в зависимости от битов ключа
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
                // ЧТЕНИЕ WAV АУДИОФАЙЛА
                using (var song = new NAudio.Wave.WaveFileReader(hidden_file))
                {
                    // cоздаем новый массив байтов для хранения содержимого аудиофайла
                    var frame_bytes = new byte[song.Length];
                    // cчитываем содержимое аудиофайла в массив байтов, начиная с индекса 0
                    song.Read(frame_bytes, 0, frame_bytes.Length);

                    // КОНВЕРТИРОВАНИЕ КЛЮЧА В БИТОВЫЙ МАССИВ
                    // конкатенация каждого символа сообщения как двоичной строки, заполненную 0 слева, чтобы получилось 8 цифр
                    var bits_string_key_word = string.Concat(keyword.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
                    // преобразование каждого символа двоичной строки в целое число и создание списка целых чисел
                    var bits_key_word = bits_string_key_word.Select(c => Convert.ToInt32(c.ToString())).ToList();

                    var extracted = new List<int>();
                    var n = 0;
                    var i = 0;
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
                    }

                    var bit_string = string.Concat(extracted.Select(bit => bit.ToString()));
                    var decoded = "";
                    int o = 0;
                    for (var j = 0; j < bit_string.Length; j += 8)
                    {
                        if(o == 3)
                        {
                            break;
                        }
                        var bits = bit_string.Substring(j, 8);
                        var letter_number = Convert.ToInt32(bits, 2);
                        var letter = ((char)letter_number).ToString();
                        if (letter == "#")
                        {
                            o++;
                        }
                        else
                        {
                            decoded += letter;
                        }
                    }

                    if(n == 0)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        string result = decoded;

                        // формирование названия нового файла
                        string outputFile = hidden_file.Replace(".wav", "_extracted.txt");

                        // запись пути сохранения нового аудиофайла в глобальную переменную
                        GlobalClass.ExtractFileResult = outputFile;

                        using (var stream = new StreamWriter(outputFile, false))
                        {
                            stream.Write(result, 0, result);
                        }
                    }
                }
            }
            catch (IOException)
            {
                // ЕСЛИ ИСХОДНЫЙ WAV АУДИО ФАЙЛ НЕ НАЙДЕН
                MessageBox.Show($"Невозможно открыть WAV-файл.\nУказанное расположение: {GlobalClass.WAVfile}.\nНайдите и выберите существующий файл.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException)
            {
                // ЕСЛИ КЛЮЧ НЕПРАВИЛЬНЫЙ
                MessageBox.Show($"Невозможно расшифровать WAV-файл.\nНеправильный ключ.\nВы ввели: {GlobalClass.secretKey}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
