import 'package:app/models/plant.dart';
import 'package:flutter/material.dart';
import 'package:app/constants.dart';

import 'image_and_icons.dart';
import 'title_and_configuration.dart';

class Body extends StatelessWidget {
  final Plant plant;

  const Body({Key key, this.plant}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;
    return SingleChildScrollView(
      child: Column(
        children: <Widget>[
          ImageAndIcons(size: size, measurement: plant.measurement?.first, image: plant.image),
          TitleAndConfiguration(plantName: plant.name, irrigationConfig: plant.irrigationConfig,),
          const SizedBox(height: kDefaultPadding),
        ],
      ),
    );
  }
}
