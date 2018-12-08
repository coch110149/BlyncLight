using System.Collections.Generic;

namespace BlyncN
{
  public class ColorController
  {
    private BlyncController _con;
    private List<LightColor> _colors;
    private int _deviceCount;
    private int _colorIndex;

    public ColorController()
    {
      _colors = new List<LightColor>();
      _con = new BlyncController();
      _deviceCount = _con.InitBlyncDevices();
      _colorIndex = -1;
    }

    public int DeviceCount
    {
      get { return _deviceCount; }
    }

    private BlyncController.Color Map(LightColor color)
    {
      BlyncController.Color ret;
      switch(color)
      {
        case LightColor.Cyan: ret = BlyncController.Color.Cyan; break;
        case LightColor.White: ret = BlyncController.Color.White; break;
        case LightColor.Blue: ret = BlyncController.Color.Blue; break;
        case LightColor.Yellow: ret = BlyncController.Color.Yellow; break;
        case LightColor.Green: ret = BlyncController.Color.Green; break;
        case LightColor.Red: ret = BlyncController.Color.Red; break;
        case LightColor.Purple: ret = BlyncController.Color.Purple; break;
        
        default: ret = BlyncController.Color.Off; break;
      }
      return ret;
    }

    public void Add(LightColor color)
    {
      _colors.Add(color);
      _colorIndex = 0;
    }

    public void Clear()
    {
      _colors.Clear();
      _colorIndex = -1;
    }

    public List<LightColor> Recipe
    {
      get 
      {
        List<LightColor> ret = new List<LightColor>();
        foreach (LightColor color in _colors)
          ret.Add(color);
        return ret;
      }

      set
      {
        _colorIndex = -1;
        _colors = value;
        _colorIndex = 0;
      }
    }

    public void Off()
    {
      if (_con != null) _con.Display(Map(LightColor.Off));
    }

    public void On(LightColor color)
    {
      if (_con != null) _con.Display(Map(color));
    }

    public void SetColor()
    {
      if (_con != null)
      {
        if (_colorIndex > -1)
        {
          _con.Display(Map(_colors[_colorIndex]));
          _colorIndex++;
          if (_colorIndex >= _colors.Count) _colorIndex = 0;
        }
      }
    }
  }
}
