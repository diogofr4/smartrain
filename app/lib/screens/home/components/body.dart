import 'package:app/constants.dart';
import 'package:app/models/plant.dart';
import 'package:flutter/material.dart';

import 'header_with_searchbox.dart';
import 'created_plants.dart';
import 'title_with_more_btn.dart';

class Body extends StatelessWidget {
  const Body({Key key, this.plantList}) : super(key: key);
  final List<Plant> plantList;

  @override
  Widget build(BuildContext context) {
    Widget createdPlants = const CreatedPlants();
    if(plantList != null) {
      createdPlants = CreatedPlants(plantsList: plantList,);
    }

    return Column(
        children: <Widget>[
          HeaderWithSearchBox(size: MediaQuery.of(context).size),
          const TitleWithMoreBtn(title: "Plantas cadastradas"),
          Expanded(child: createdPlants),
          const SizedBox(height: kDefaultPadding),
        ],
      );
  }
}
