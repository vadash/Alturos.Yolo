using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Alturos.Yolo
{
    public class YoloTracking
    {
        private YoloWrapper _yoloWrapper;
        private Point _trackingObject;
        private int _maxDistance;

        public YoloTracking(YoloWrapper yoloWrapper, int maxDistance = 1000)
        {
            this._yoloWrapper = yoloWrapper;
            this._maxDistance = maxDistance;
        }

        public void SetTrackingObject(YoloItem trackingObject)
        {
            this._trackingObject = trackingObject.Center();
        }

        public void SetTrackingObject(Point trackingObject)
        {
            this._trackingObject = trackingObject;
        }

        public YoloItem Analyse(byte[] imageData)
        {
            var items = this._yoloWrapper.Detect(imageData);

            var probableObject = this.FindBestMatch(items, this._maxDistance);
            if (probableObject == null)
            {
                return null;
            }

            this._trackingObject = probableObject.Center();
            return probableObject;
        }

        private YoloItem FindBestMatch(IEnumerable<YoloItem> items, int maxDistance)
        {
            var distanceItems = items.Select(o => new { Distance = this.Distance(o.Center(), this._trackingObject), Item = o }).Where(o => o.Distance <= maxDistance).OrderBy(o => o.Distance);

            var bestMatch = distanceItems.FirstOrDefault();
            return bestMatch?.Item;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(this.Pow2(p2.X - p1.X) + Pow2(p2.Y - p1.Y));
        }

        private double Pow2(double x)
        {
            return x * x;
        }
    }
}
