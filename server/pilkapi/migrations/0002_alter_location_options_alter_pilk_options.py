# Generated by Django 5.0.1 on 2024-10-03 02:53

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('pilkapi', '0001_initial'),
    ]

    operations = [
        migrations.AlterModelOptions(
            name='location',
            options={'ordering': ['name']},
        ),
        migrations.AlterModelOptions(
            name='pilk',
            options={'ordering': ['name']},
        ),
    ]
