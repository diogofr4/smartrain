import 'dart:convert';
import 'dart:io';

import 'package:app/models/plant.dart';
import 'package:app/screens/menu/create_plant.dart';
import 'package:fab_circular_menu/fab_circular_menu.dart';
import 'package:flutter/material.dart';
import 'package:app/screens/details/components/body.dart';
import 'package:http/io_client.dart';
import 'package:http/http.dart' as http;
import '../../constants.dart';

class DetailsScreen extends StatefulWidget{
  const DetailsScreen({Key key, this.plant}) : super(key: key);

  final Plant plant;
  @override
  State<StatefulWidget> createState() => _DetailsScreenState();
}

class _DetailsScreenState extends State<DetailsScreen> {
  Future<List<String>> getAvailableSensors() async {
    HttpClient client = HttpClient()..badCertificateCallback = ((X509Certificate cert, String host, int port) => true);;
    var ioClient = IOClient(client);
    http.Response response = await ioClient.get(Uri.parse('https://192.168.15.118:45455/User/GetAvailableSensors'));

    if(response.statusCode == 200){
      var parsed = jsonDecode(response.body);
      return List<String>.from(parsed);
    }
    else{
      throw Exception('Failed to load sensor list');
    }
  }

  Future<bool> deletePlant() async {
    HttpClient client = HttpClient()..badCertificateCallback = ((X509Certificate cert, String host, int port) => true);
    try{
      var request = await client.deleteUrl(Uri.parse('https://192.168.15.118:45455/User/DeletePlant?plantId=${widget.plant.id}'));
      var response = await request.close();

      if(response.statusCode == 200){
        return true;
      }
      else {
        return false;
      }
    }
    finally{
      client.close();
    }
  }

  List<String> _sensorList;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: FutureBuilder(
          future: getAvailableSensors(),
          builder: (context, snapshot){
            if(snapshot.hasData)
              _sensorList = snapshot.data;
              return Body(plant: widget.plant,);
          },
        ),
        floatingActionButton: FabCircularMenu(
          ringDiameter: MediaQuery.of(context).size.height / 4,
            fabOpenIcon: Icon(Icons.settings, color: Colors.white,),
            fabCloseIcon: Icon(Icons.close, color: Colors.white,),
            ringColor: Color.fromRGBO(12, 152, 105, 0.7),
            fabColor: kPrimaryColor,
            children: <Widget>[
              IconButton(icon: Icon(Icons.edit, color: Colors.white), onPressed: () {
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) => SerializedForm(sensorList: _sensorList, plant: widget.plant)
                    )
                ).then((value) => setState(() {}));
              }),
              IconButton(icon: Icon(Icons.delete, color: Colors.white,), onPressed: () {
                showDialog<String>(
                  context: context,
                  builder: (BuildContext context) => AlertDialog(
                    title: const Text('Confirmação'),
                    content: const Text('Tem certeza que deseja excluir esta planta?'),
                    actions: <Widget>[
                      TextButton(
                        onPressed: () => Navigator.pop(context),
                        child: const Text('Não'),
                      ),
                      TextButton(
                        onPressed: () async{
                          int count = 0;
                          await deletePlant();
                          Navigator.of(context).popUntil((_) => count++ >= 2);
                        },
                        child: const Text('Sim'),
                      ),
                    ],
                  ),
                );
              })
            ]
        )
    );
  }
}
