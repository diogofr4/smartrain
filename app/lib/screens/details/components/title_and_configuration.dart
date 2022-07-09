import 'package:app/models/irrigation_config.dart';
import 'package:app/screens/shared_components/icon_card.dart';
import 'package:flutter/material.dart';

import '../../../constants.dart';

class TitleAndConfiguration extends StatelessWidget {
  const TitleAndConfiguration({
    Key key,
    this.plantName,
    this.irrigationConfig,
  }) : super(key: key);

  final IrrigationConfig irrigationConfig;
  final String plantName;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: kDefaultPadding),
      child: Column(
        children: <Widget>[
          DefaultTextStyle(
            style: Theme.of(context)
            .textTheme
            .headline4
            ?.copyWith(color: kTextColor, fontWeight: FontWeight.bold),
            child: Text("$plantName\n".toUpperCase(),),
          ),
          DefaultTextStyle(
            style: Theme.of(context)
                .textTheme
                .headline6
                ?.copyWith(color: kTextColor, fontWeight: FontWeight.bold),
            child: Text("Configurações de Irrigação"),
          ),
          const SizedBox(height: 20),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: <Widget>[
              Column(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  DefaultTextStyle(
                    style: Theme.of(context)
                        .textTheme
                        .subtitle1
                        ?.copyWith(color: kTextColor, fontWeight: FontWeight.bold),
                    child: Text("Umidade mínima"),
                  ),
                  IconCard(
                      icon: "assets/icons/humidity_icon.svg",
                      iconHeight: 62,
                      backgroundColor: kBackgroundColor,
                      textColor: kPrimaryColor,
                      text: irrigationConfig.humidityMin.toString() ?? ""
                  ),
                ],
              ),
              Column(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  DefaultTextStyle(
                    style: Theme.of(context)
                        .textTheme
                        .subtitle1
                        ?.copyWith(color: kTextColor, fontWeight: FontWeight.bold),
                    child: Text("Temperatura máxima"),
                  ),
                  IconCard(
                      icon: "assets/icons/temperature_icon.svg",
                      iconHeight: 62,
                      backgroundColor: kBackgroundColor,
                      textColor: kPrimaryColor,
                      text: irrigationConfig.temperatureMax.toString() ?? ""
                  ),
                ],
              ),
            ],
          )
        ],
      ),
    );
  }
}
