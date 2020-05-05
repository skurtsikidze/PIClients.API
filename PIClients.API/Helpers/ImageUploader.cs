using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PIClients.API.Helpers
{
  public class ImageUploader : IDisposable
  {
    bool _disposed;

    private byte[] _image;

    public ImageUploader(string image)
    {
      try
      {
        if (image != null && !String.IsNullOrEmpty(image))
          _image = Convert.FromBase64String(image);
      }
      catch
      {
        _image = null;
      }
    }

    public string SaveFile(string path)
    {
      string fileName = "";

      try
      {
        if (_image != null)
        {
          fileName = Guid.NewGuid().ToString() + ".jpg";
          path = path + "\\Uploads\\" + fileName;
          File.WriteAllBytes(path, _image);
        }
        else
          path = String.Empty;
      }
      catch
      {
        path = String.Empty;
      }

      return path;
    }

    #region *** IDisposable members

    public void Dispose()
    {
      Dispose(true);
    }

    public void Dispose(bool disposing)
    {
      if (_disposed) return;

      if (disposing)
      {
        _image = null;
      }

      _disposed = true;
    }

    #endregion
  }
}
