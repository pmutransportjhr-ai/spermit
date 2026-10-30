using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for FileUploadValidations
/// </summary>
public class FileUploadValidations
{
    private string GetActualFileType(string filename, Stream stream)
    {
        try
        {
            System.IO.BinaryReader sr = new System.IO.BinaryReader(stream);
            
                byte[] header = new byte[16];
                sr.Read(header, 0, 16);


                StringBuilder hexString = new StringBuilder(header.Length);
                for (int i = 0; i < header.Length; i++)
                {
                    hexString.Append(header[i].ToString("X2"));
                }


                string FileTypeCode = hexString.ToString();

                if (FileTypeCode.StartsWith("255044462D312"))
                {
                    if (stream.Length <= 0 || stream.Length > 2097152)
                    {
                        return filename + " must be greater than 0 MB and less than 2 MB";
                    }
                    else
                    {
                        return "OK";
                    }

                }
                else
                {
                    return filename + " is not valid PDF File !";
                }
            
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
            
    private string DoubleFileExtension(string filename, Stream filecontent)
    {
        string response = string.Empty;
        string[] presenceofDot = filename.Split('.');
        if (presenceofDot.Length > 2)
        {
           // return response = "File Name has more than 1 extenstion";
            return response = "Please upload valid " + filename + " pdf file";

        }
        else
        {
            return GetActualFileType(filename, filecontent);
        }

    }
            
    private string FileNameMetaChar(string filename, Stream filecontent)
    {
        if (filename.IndexOfAny("+#%- ".ToCharArray()) < 0)
        {
            if (filename.Length < 100)
            {
                return DoubleFileExtension(filename, filecontent);

            }
            else
            {
                return filename + "Must be less than 100 Char.";
            }
        }
        else
            return "Invalid character in " + filename;


    }

    public string FileValidate(string filename, Stream filecontent)
    {
        return FileNameMetaChar(filename, filecontent);
    }

}