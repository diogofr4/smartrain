import 'dart:convert';
import 'dart:io';

import 'package:app/models/plant.dart';
import 'package:app/screens/details/details_screen.dart';
import 'package:app/screens/shared_components/icon_card.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:http/io_client.dart';
import 'package:intl/intl.dart';

import '../../../constants.dart';


class CreatedPlants extends StatefulWidget {
  const CreatedPlants({
    Key key, this.plantsList,
  }) : super(key: key);

  final List<Plant> plantsList;

  @override
  CreatedPlantsState createState() => CreatedPlantsState();
}

class CreatedPlantsState extends State<CreatedPlants>{
  @override
  Widget build(BuildContext context) {
    if(widget.plantsList == null || widget.plantsList.isEmpty){
      return Container(
          height: MediaQuery.of(context).size.height/2,
          alignment: Alignment.center,
          child: RichText(
            text: TextSpan(
              children: [
                TextSpan(
                    text: "Clique no ", style: Theme.of(context).textTheme.button
                ),
                const WidgetSpan(
                  child: Icon(Icons.add, size: 20),
                ),
                TextSpan(
                    text: " para criar uma planta", style: Theme.of(context).textTheme.button
                ),
              ],
            ),
          )
      );
    }

    return ListView.builder(
      itemCount: widget.plantsList.length,
      itemBuilder: (context, i){
        return _buildRow(widget.plantsList[i],
                () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => DetailsScreen(plant: widget.plantsList[i],),
                ),
              );
            },
            context
        );
      },
    );
  }
}

Widget _buildRow(Plant plant, Function press, BuildContext context) {
  Size size = MediaQuery.of(context).size;
  Text lastUpdate;
  if(plant.measurement == null) {
    lastUpdate = const Text("Nunca recebeu atualização",style: TextStyle(color: kPrimaryColor, fontSize: 12));
  }
  else{
    lastUpdate = Text(
        "Atualizado em: ${DateFormat("dd/MM/yyyy hh:mm").format(plant.measurement.first.readingDateTime)}\n".toUpperCase(),
        style: const TextStyle(color: kPrimaryColor, fontSize: 12));
  }
  return GestureDetector(
    onTap: press,
      child:Container(
          width: size.width * 0.4,
          margin: const EdgeInsets.only(
          left: kDefaultPadding,
          right: kDefaultPadding,
          bottom: kDefaultPadding,
          ),

      child: Column(
      children: <Widget>[
        Container(
          width: MediaQuery.of(context).size.width,
          height: MediaQuery.of(context).size.height / 4,
          decoration: BoxDecoration(
            image: DecorationImage(
              fit: BoxFit.fill,
              image: AssetImage(plant.image),
            ),
          ),
        ),
        Container(
            padding: const EdgeInsets.all(kDefaultPadding / 2),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: const BorderRadius.only(
                bottomLeft: Radius.circular(10),
                bottomRight: Radius.circular(10),
              ),
              boxShadow: [
                BoxShadow(
                  offset: const Offset(0, 10),
                  blurRadius: 50,
                  color: kPrimaryColor.withOpacity(0.23),
                ),
              ],
            ),
            child: Column(
              children: <Widget>[
                Text("${plant.name}\n".toUpperCase(), style: Theme.of(context).textTheme.button,),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconCard(icon: "assets/icons/humidity_icon.svg", iconHeight: 55, backgroundColor: Colors.white, textColor: kPrimaryColor, text: plant.measurement?.first?.humidity ?? ""),
                    IconCard(icon: "assets/icons/luminosity_icon.svg", iconHeight: 55, backgroundColor: Colors.white, textColor: kPrimaryColor, text: plant.measurement?.first?.luminosity ?? ""),
                    IconCard(icon: "assets/icons/temperature_icon.svg", iconHeight: 55, backgroundColor: Colors.white, textColor: kPrimaryColor, text: plant.measurement?.first?.temperature ?? ""),
                  ],
                ),
                Align(
                  alignment: Alignment.centerRight,
                    child: lastUpdate,
                ),
              ],
            ),
          ),
      ],
    ),
  ));
}