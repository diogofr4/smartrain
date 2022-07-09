class Measurement{
  String _humidity;
  String _temperature;
  String _luminosity;
  bool _rain;
  DateTime _readingDateTime;


  Measurement(this._readingDateTime, this._humidity, this._rain, this._temperature, this._luminosity);

  factory Measurement.fromJson(Map<String, dynamic> json) {
    return Measurement(
        DateTime.parse(json['readingDateTime']),
        "${json['humidity']}%",
        json['rain'].toLowerCase() == 'true',
        "${json['temperature']}º",
        "${json['luminosity']}%"
    );
  }

  DateTime get readingDateTime => _readingDateTime;

  set readingDateTime(DateTime value) {
    _readingDateTime = value;
  }

  bool get rain => _rain;

  set rain(bool value) {
    _rain = value;
  }

  String get luminosity => _luminosity;

  set luminosity(String value) {
    _luminosity = value;
  }

  String get temperature => _temperature;

  set temperature(String value) {
    _temperature = value;
  }

  String get humidity => _humidity;

  set humidity(String value) {
    _humidity = value;
  }
}