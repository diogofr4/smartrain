import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:app/models/plant.dart';
import 'package:app/screens/menu/create_plant.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/svg.dart';
import 'package:http/io_client.dart';
import 'package:http/http.dart' as http;
import '../../constants.dart';
import 'components/body.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({Key key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen>{
  Future<List<String>> getAvailableSensors() async {
    HttpClient client = HttpClient()..badCertificateCallback = ((X509Certificate cert, String host, int port) => true);
    var ioClient = IOClient(client);
    http.Response response = await ioClient.get(Uri.parse('https://192.168.15.118:45455/User/GetAvailableSensors'));

    if(response.statusCode == 200){
      var parsed = jsonDecode(response.body);
      return List<String>.from(parsed);
    }
    else{
      throw Exception('Failed to load plant list');
    }
  }

  Future<List<Plant>> getPlantsList() async {
    HttpClient client = HttpClient()..badCertificateCallback = ((X509Certificate cert, String host, int port) => true);;
    var ioClient = IOClient(client);
    http.Response response = await ioClient.get(Uri.parse('https://192.168.15.118:45455/User/GetPlants'));

    if(response.statusCode == 200){
      var parsed = jsonDecode(response.body);
      return List<Plant>.from(parsed.map((model) => Plant.fromJson(model)));
    }
    else{
      throw Exception('Failed to load plant list');
    }
  }

  List<String> _sensorList;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: FutureBuilder(
          future: Future.wait([getPlantsList(), getAvailableSensors()]),
          builder: (context, snapshot){
            if(snapshot.hasData) {
              _sensorList = snapshot.data[1];
              return Body(plantList: snapshot.data[0]);
            }

            return const Body();
          },
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: (){
            Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => SerializedForm(sensorList: _sensorList)
              )
            ).then((value) => setState(() {}));
          },
          backgroundColor: kPrimaryColor,
          child: const Icon(Icons.add),
        )
    );
  }
}
