using System;
using FFImageLoading.Forms;
using System.Collections.Generic;
using FFImageLoading.Work;
using FFImageLoading.Transformations;

namespace AppCreator.Custom {
	public class CircleImage : CachedImage {
		public CircleImage() {
			Transformations = new List<ITransformation> { new CircleTransformation() };
		}
	}
}

