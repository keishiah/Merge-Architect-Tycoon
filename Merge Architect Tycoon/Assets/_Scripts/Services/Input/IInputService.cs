using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services
{
  public interface IInputService 
  {
    Vector3 Axis { get; }
  }
}