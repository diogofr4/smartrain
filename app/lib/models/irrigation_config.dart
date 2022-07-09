import 'package:flutter/foundation.dart';

class IrrigationConfig{
  int humidityMin;
  int temperatureMax;

  IrrigationConfig([this.humidityMin, this.temperatureMax]);

  factory IrrigationConfig.fromJson(Map<String, dynamic> json){
    return IrrigationConfig(
      json["humidityMin"],
      json["temperatureMax"],
    );
  }

}