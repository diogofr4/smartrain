import 'dart:convert';
import 'dart:io';

import 'package:app/models/plant.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_bloc/flutter_form_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/io_client.dart';
import 'package:http/http.dart' as http;

class SerializedFormBloc extends FormBloc<String, String> {
  InputFieldBloc<int, Object> plantId;

  final plantName = TextFieldBloc(
    name: 'plantName',
  );

  SelectFieldBloc<String, dynamic> sensorSelect;

  final minHumidity = InputFieldBloc<double, Object>(
    name: 'minHumidity',
    initialValue: 0,
  );

  final maxTemperature = InputFieldBloc<double, Object>(
    name: 'maxTemperature',
    initialValue: 0,
  );

  void setFieldBlocs(List<String> sensorList, [Plant plant]){
    sensorSelect = SelectFieldBloc(
      items: sensorList,
      name: 'sensorId'
    );

    if(plant != null) {
      plantId = InputFieldBloc(
          name: 'plantId',
          initialValue: plant.id
      );

      plantName.updateInitialValue(plant.name);
      sensorSelect.updateInitialValue(plant.sensor);
      sensorSelect.addItem(plant.sensor);
      minHumidity.updateInitialValue(plant.irrigationConfig.humidityMin.toDouble());
      maxTemperature.updateInitialValue(plant.irrigationConfig.temperatureMax.toDouble());
      addFieldBloc(fieldBloc: plantId);
    }

    addFieldBlocs(
      fieldBlocs: [
        plantName,
        sensorSelect,
        minHumidity,
        maxTemperature
      ],
    );
  }

  @override
  void onSubmitting() async {
    HttpClient client = HttpClient()..badCertificateCallback = ((X509Certificate cert, String host, int port) => true);
    try {
      var url = Uri.parse('https://192.168.15.118:45455/User/CreatePlant');
      var successMessage = "Planta criada com sucesso!";
      var failureMessage = "Não foi possível realizar a criação da planta";
      var request = await client.postUrl(url);
      if(plantId != null){
        url = Uri.parse('https://192.168.15.118:45455/User/EditPlant?plantId=${plantId.value}');
        successMessage = "Planta editada com sucesso!";
        failureMessage = "Não foi possível realizar a edição da planta";
        request = await client.putUrl(url);
      }

      request.headers.set(HttpHeaders.contentTypeHeader, "application/json; charset=UTF-8");
      request.write(const JsonEncoder.withIndent('    ').convert(state.toJson()));
      var response = await request.close();

      if (response.statusCode == 200) {
        emitSuccess(
          canSubmitAgain: false,
          successResponse: successMessage,
        );
      }
      else {
        emitFailure(
            failureResponse: "Não foi possível realizar a criação da planta"
        );
      }
    }
    finally{
      client.close();
    }
  }
}

class SerializedForm extends StatelessWidget {
  const SerializedForm({Key key, this.sensorList, this.plant}) : super(key: key);

  final Plant plant;
  final List<String> sensorList;

  @override
  Widget build(BuildContext context) {
    var formBloc = SerializedFormBloc();
    var title = plant == null ? 'Cadastrar planta' : 'Editar planta';
    formBloc.setFieldBlocs(sensorList, plant);

    return BlocProvider(
      create: (context) => formBloc,
      child: Builder(
        builder: (context) {
          final formBloc = context.read<SerializedFormBloc>();

          return Theme(
            data: Theme.of(context).copyWith(
              inputDecorationTheme: InputDecorationTheme(
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(20),
                ),
              ),
            ),
            child: Scaffold(
              resizeToAvoidBottomInset: false,
              appBar: AppBar(title: Text(title)),
              floatingActionButton: FloatingActionButton(
                onPressed: formBloc.submit,
                child: const Icon(Icons.send),
              ),
              body: FormBlocListener<SerializedFormBloc, String, String>(
                onSuccess: (context, state) {
                  ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                    content: Text(state.successResponse),
                    duration: const Duration(seconds: 2),
                  ));
                  Navigator.pop(context);
                },
                child: SingleChildScrollView(
                  physics: const ClampingScrollPhysics(),
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Column(
                      children: <Widget>[
                        TextFieldBlocBuilder(
                          textFieldBloc: formBloc.plantName,
                          keyboardType: TextInputType.emailAddress,
                          decoration: const InputDecoration(
                            labelText: 'Nome da planta',
                            prefixIcon: Icon(Icons.eco_outlined),
                          ),
                        ),
                        DropdownFieldBlocBuilder<String>(
                          selectFieldBloc: formBloc.sensorSelect,
                          decoration: const InputDecoration(
                            labelText: 'Sensor',
                            prefixIcon: Icon(Icons.developer_board_outlined),
                          ),
                          itemBuilder: (context, value) => FieldItem(
                            child: Text(value),
                          ),
                        ),
                        DefaultTextStyle (
                          style: Theme.of(context).textTheme.button,
                          child: const Text("Configurações de Irrigação"),
                        ),
                        SliderFieldBlocBuilder(
                          min: 0,
                          max: 100,
                          divisions: 100,
                          labelBuilder: (context, value) => "${value.toStringAsFixed(0)}%",
                          inputFieldBloc: formBloc.minHumidity,
                          decoration: InputDecoration(
                            labelText: 'Umidade mínima',
                            prefixIcon: SvgPicture.asset("assets/icons/humidity_icon.svg", color: Colors.grey),
                          ),
                        ),
                        SliderFieldBlocBuilder(
                          min: 0,
                          max: 100,
                          inputFieldBloc: formBloc.maxTemperature,
                          labelBuilder: (context, value) => "${value.toStringAsFixed(0)}º",
                          divisions: 100,
                          decoration: InputDecoration(
                            labelText: 'Temperatura máxima',
                            prefixIcon: SvgPicture.asset("assets/icons/temperature_icon.svg",color: Colors.grey),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}
