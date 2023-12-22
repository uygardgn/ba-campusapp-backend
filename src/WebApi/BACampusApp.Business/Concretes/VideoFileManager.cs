using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using BACampusApp.DataAccess.Migrations;
using Microsoft.AspNetCore.Components.Forms;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;


namespace BACampusApp.Business.Concretes
{

    public class VideoFileManager : IVideoFileService
    {
        TimeSpan maxDuration = TimeSpan.FromSeconds(15); // 15 sn süre sınırı
        /// <summary>
        /// Alınan videonun süresini kontrol eder ve buna göre bir değer döner
        /// </summary>
        /// <param name="videoFile"></param>
        /// <returns></returns>
        public async Task<bool> CheckVideoDurationAsync(IFormFile videoFile)
        {
            try
            {
                using (var stream = videoFile.OpenReadStream())
                {
                    var mediaInfo = await FFProbe.AnalyseAsync(stream);
                    var duration = mediaInfo.Duration;

                    if (duration > maxDuration)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Alınan videonun kalitesini 480p ye indirir
        /// </summary>
        /// <param name="videoFile"></param>
        /// <returns></returns>
        public async Task<IFormFile> ConvertTo480pAsync(IFormFile videoFile)
        {
            try
            {
                var tempDirectory = Path.Combine(Environment.CurrentDirectory, "wwwroot", "documents", "TempVideos");
                Directory.CreateDirectory(tempDirectory);

                using (var stream = videoFile.OpenReadStream())
                {
                    var tempFilePath = Path.Combine(tempDirectory, $"{Guid.NewGuid()}_temp.mp4");

                    // Geçici dosyaya kaydet
                    using (var tempFileStream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(tempFileStream);
                    }

                    // Video dönüştürme işlemi
                    var outputPath = Path.Combine(tempDirectory, $"{Guid.NewGuid()}_converted_video.mp4");
                    await FFMpegArguments
                            .FromFileInput(tempFilePath)
                            .OutputToFile(outputPath, false, options => options
                            .WithVideoCodec(VideoCodec.LibX264)
                            .WithConstantRateFactor(21)
                            .WithAudioCodec(AudioCodec.Aac)
                            .WithVariableBitrate(4)
                            .WithVideoFilters(filterOptions => filterOptions
                            .Scale(854, 480)) // 480p için ayarlandı. Farklı formatlar için burası değiştirilecek.
                            .WithFastStart())
                            .ProcessAsynchronously();

                    // Dönüştürülen dosyayı oku ve IFormFile olarak geri döndür
                    var convertedVideoBytes = await File.ReadAllBytesAsync(outputPath);
                    var convertedVideoStream = new MemoryStream(convertedVideoBytes);

                    // Geçici dosyaları sil
                    File.Delete(tempFilePath);
                    File.Delete(outputPath);

                    // Orijinal dosyanın ContentType'ını kullanarak FormFile oluştur
                    return new FormFile(convertedVideoStream, 0, convertedVideoBytes.Length, "converted_video", "converted_video.mp4")
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = videoFile.ContentType
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private const long MaxFileSizeInBytes = 10 * 1024 * 1024; // 10 mb sınır
        /// <summary>
        /// Metota gelen videonun boyutunu kontrol eder. Mevcut kurguda boyutu değiştirilen video buraya gönderilmekte.
        /// </summary>
        /// <param name="videoFile"></param>
        /// <returns></returns>
        public async Task<bool> CheckVideoSizeAsync(IFormFile videoFile)
        {
            try
            {
                using (var stream = videoFile.OpenReadStream())
                {
                    if (stream.Length > MaxFileSizeInBytes)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> ConvertTo360pAsync(byte[] videoBytes)
        {
            try
            {
                var tempDirectory = Path.Combine(Environment.CurrentDirectory, "wwwroot", "documents", "TempVideos");
                Directory.CreateDirectory(tempDirectory);

                var tempFilePath = Path.Combine(tempDirectory, $"{Guid.NewGuid()}_temp.mp4");

                // Write byte array to a temporary file
                await File.WriteAllBytesAsync(tempFilePath, videoBytes);

                // Video conversion process
                var outputPath = Path.Combine(tempDirectory, $"{Guid.NewGuid()}_converted_video.mp4");
                await FFMpegArguments
                        .FromFileInput(tempFilePath)
                        .OutputToFile(outputPath, false, options => options
                        .WithVideoCodec(VideoCodec.LibX264)
                        .WithConstantRateFactor(21)
                        .WithAudioCodec(AudioCodec.Aac)
                        .WithVariableBitrate(4)
                        .WithVideoFilters(filterOptions => filterOptions
                        .Scale(640, 360))
                        .WithFastStart())
                        .ProcessAsynchronously();

                // Read the converted file into a byte array
                var convertedVideoBytes = await File.ReadAllBytesAsync(outputPath);

                // Delete temporary files
                File.Delete(tempFilePath);
                File.Delete(outputPath);

                // Return the converted byte array
                return convertedVideoBytes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}




