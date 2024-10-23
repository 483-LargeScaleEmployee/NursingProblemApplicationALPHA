/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
using Microsoft.Maui.Graphics;

namespace NursingProblemApplicationALPHA;
class MainDrawable : IDrawable
{
    private MainViewModel _vm;
    private MainViewModel VM
        => _vm ??= App.Current.MainPage.Handler.MauiContext.Services.GetService<MainViewModel>();
                   //ServiceHelper.GetService<MainViewModel>();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        VM.Cube.Draw(canvas, dirtyRect, VM.Mode);
    }
}
