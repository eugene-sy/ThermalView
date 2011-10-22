using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ThermalView.Entities;
using ThermalView.Views.AlertDialogs;

namespace ThermalView.Controllers
{
    class SolverController
    {
        private ObservableCollection<Area> _areaCollection;
        private ObservableCollection<ResultSquare> _resultSquares;
        private ObservableCollection<string> _colors;
        private ObservableCollection<int> _colorNumbers;
        private ObservableCollection<GraphDataEntry> _graphData;
        private int _bitsPerPix;

        public SolverController(ObservableCollection<Area> areas, int bitsPerPixel)
        {
            _areaCollection = areas;
            _resultSquares = new ObservableCollection<ResultSquare>();
            _colors = new ObservableCollection<string>();
            _colorNumbers = new ObservableCollection<int> {0, 0, 0, 0, 0, 0, 0, 0};
            _bitsPerPix = bitsPerPixel;
        }

        public void Solve()
        {
            var mainArea = _areaCollection.Where(area => area.Main);
            
            var lenght = mainArea.Aggregate<Area, double>(0, (current, area) => current + area.Points.Count);

            var otherAreas = _areaCollection.Where(area => !area.Main);

            foreach (var result in otherAreas.Select(otherArea => new ResultSquare
                                                                      {
                                                                          AreaName = otherArea.Name, 
                                                                          squarePerSent = Math.Round(otherArea.Points.Count/lenght*100, 2)
                                                                      }))
            {
                _resultSquares.Add(result);
            }

            _resultSquares.Add(new ResultSquare
                                   {
                                       AreaName = "Основная область",
                                       squarePerSent = 100
                                   });

            MakeGraphData();
            // this is for porn!
            /*try
            {
                Random r = new Random();
                if (r.NextDouble() < 0.1)
                {
                    throw new NullReferenceException();
                }
            }
            catch(NullReferenceException e)
            {
                var alert = new AlertDialog("Произошла ошибка" + e);
                alert.Show();
                return;
            }*/
        }

        public ObservableCollection<ResultSquare> GetResultSquares()
        {
            return _resultSquares;
        }

        private void MakeGraphData()
        {
            var setController = new SettingsController();

            _colors = setController.Settings.ColorList;

            foreach (var area in _areaCollection)
            {
                
            }
        }
    }
}
