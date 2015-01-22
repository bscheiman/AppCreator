﻿using System;
using Newtonsoft.Json;

namespace AppCreator.Json {
	public class BaseJsonObject {
		[JsonProperty("error")]
		public bool Error { get; set; }

		[JsonProperty("exception")]
		public Exception Exception { get; set; }
	}
}
