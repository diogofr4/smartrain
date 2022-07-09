import 'package:app/models/irrigation_config.dart';

import 'measurement.dart';

class Plant{
  int _id;
  String _name;
  String _image = "assets/images/image_1.png";
  List<Measurement> _measurement;
  IrrigationConfig irrigationConfig;
  String _sensor;

  Plant(this._id, this._name, this._measurement, this._sensor, this.irrigationConfig);

  factory Plant.fromJson(Map<String, dynamic> json) {
    var measurementJson = json['measurements'];
    List<Measurement> measurement;
    if(measurementJson[0] != null) {
      measurement = List<Measurement>.from(json['measurements'].map((model) => Measurement.fromJson(model)));
    }

    var irrigationConfig = IrrigationConfig.fromJson(json["irrigationConfig"]);
    return Plant(
        json['plantId'],
        json['plantName'],
        measurement,
        json['sensorId'],
        irrigationConfig
    );
  }


  int get id => _id;

  set id(int value) {
    _id = value;
  }

  List<Measurement> get measurement => _measurement;

  set measurement(List<Measurement> value) {
    _measurement = value;
  }

  String get image => _image;

  set image(String value) {
    _image = value;
  }

  String get name => _name;

  set name(String value) {
    _name = value;
  }

  String get sensor => _sensor;

  set sensor(String value) {
    _sensor = value;
  }
}