import 'package:app/models/measurement.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

import '../../../constants.dart';
import '../../shared_components/icon_card.dart';

class ImageAndIcons extends StatelessWidget {
  const ImageAndIcons({
    Key key,
    this.size,
    this.measurement,
    this.image,
  }) : super(key: key);

  final Size size;
  final Measurement measurement;
  final String image;


  @override
  Widget build(BuildContext context) {
    var rainCheck = "";
    if(measurement != null) {
      rainCheck = measurement.rain ? "Sim" : "Não";
    }

    return Padding(
      padding: const EdgeInsets.only(bottom: kDefaultPadding ),
      child: SizedBox(
        height: size.height * 0.8,
        child: Row(
          children: <Widget>[
            Expanded(
              child: Padding(
                padding:
                const EdgeInsets.symmetric(vertical: kDefaultPadding * 2),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: <Widget>[
                    Align(
                      alignment: Alignment.topLeft,
                      child: IconButton(
                        padding:
                        const EdgeInsets.symmetric(horizontal: kDefaultPadding),
                        icon: SvgPicture.asset("assets/icons/back_arrow.svg"),
                        onPressed: () {
                          Navigator.pop(context);
                        },
                      ),
                    ),
                    IconCard(
                        icon: "assets/icons/luminosity_icon.svg",
                        iconHeight: 55,
                        backgroundColor: kBackgroundColor,
                        textColor: kPrimaryColor,
                        text: measurement?.luminosity ?? ""
                    ),
                    IconCard(
                        icon: "assets/icons/temperature_icon.svg",
                        iconHeight: 55,
                        backgroundColor: kBackgroundColor,
                        textColor: kPrimaryColor,
                        text: measurement?.temperature ?? ""
                    ),
                    IconCard(
                        icon: "assets/icons/humidity_icon.svg",
                        iconHeight: 55,
                        backgroundColor: kBackgroundColor,
                        textColor: kPrimaryColor,
                        text: measurement?.humidity ?? ""
                    ),
                    IconCard(
                        icon: "assets/icons/rain_icon.svg",
                        iconHeight: 55,
                        backgroundColor: kBackgroundColor,
                        textColor: kPrimaryColor,
                        text: rainCheck ?? ""
                    ),
                  ],
                ),
              ),
            ),
            Container(
              height: size.height * 0.75,
              width: size.width * 0.75,
              decoration: BoxDecoration(
                borderRadius: const BorderRadius.only(
                  topLeft: Radius.circular(63),
                  bottomLeft: Radius.circular(63),
                ),
                boxShadow: [
                  BoxShadow(
                    offset: const Offset(0, 10),
                    blurRadius: 60,
                    color: kPrimaryColor.withOpacity(0.29),
                  ),
                ],
                image: DecorationImage(
                  alignment: Alignment.centerLeft,
                  fit: BoxFit.cover,
                  image: AssetImage(image),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
