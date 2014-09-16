SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

DROP SCHEMA IF EXISTS `dta_test` ;
CREATE SCHEMA IF NOT EXISTS `dta_test` DEFAULT CHARACTER SET latin1 ;
USE `dta_test` ;

-- -----------------------------------------------------
-- Table `dta_test`.`tblAddress`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblAddress` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblAddress` (
  `idtblAddress` INT NOT NULL AUTO_INCREMENT ,
  `Address1` VARCHAR(45) NOT NULL ,
  `Address2` VARCHAR(45) NULL ,
  `City` VARCHAR(45) NOT NULL ,
  `Area` VARCHAR(45) NULL ,
  `PostCode` VARCHAR(20) NULL ,
  `Country` VARCHAR(20) NOT NULL ,
  `Phone` VARCHAR(20) NULL ,
  `Fax` VARCHAR(20) NULL ,
  PRIMARY KEY (`idtblAddress`) ,
  UNIQUE INDEX `idtblAddress_UNIQUE` (`idtblAddress` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblCompany`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblCompany` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblCompany` (
  `idtblCompany` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `AddressID` INT NULL ,
  `Notes` VARCHAR(255) NULL ,
  `Enabled` TINYINT(1) NULL ,
  PRIMARY KEY (`idtblCompany`) ,
  INDEX `fkAddressIDtblCompanytblAddress` (`AddressID` ASC) ,
  UNIQUE INDEX `idtblCompany_UNIQUE` (`idtblCompany` ASC) ,
  CONSTRAINT `fkAddressIDtblCompanytblAddress`
    FOREIGN KEY (`AddressID` )
    REFERENCES `dta_test`.`tblAddress` (`idtblAddress` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblEmployee`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblEmployee` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblEmployee` (
  `idtblEmployee` INT NOT NULL AUTO_INCREMENT ,
  `Forename` VARCHAR(45) NOT NULL ,
  `Surname` VARCHAR(45) NULL ,
  `Login` VARCHAR(20) NOT NULL ,
  `Password` VARCHAR(20) NOT NULL ,
  `RoleNumber` VARCHAR(20) NULL ,
  `Phone` VARCHAR(20) NULL ,
  `Email` VARCHAR(30) NULL ,
  `ManagerID` INT NULL ,
  `CompanyID` INT NOT NULL ,
  `Enabled` TINYINT(1) NOT NULL ,
  PRIMARY KEY (`idtblEmployee`) ,
  INDEX `fkManagerIDtblEmployeetblEmployee` (`ManagerID` ASC) ,
  INDEX `fkCompanyIDtblEmployeetblCompany` (`CompanyID` ASC) ,
  UNIQUE INDEX `idtblEmployee_UNIQUE` (`idtblEmployee` ASC) ,
  CONSTRAINT `fkManagerIDtblEmployeetblEmployee`
    FOREIGN KEY (`ManagerID` )
    REFERENCES `dta_test`.`tblEmployee` (`idtblEmployee` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkCompanyIDtblEmployeetblCompany`
    FOREIGN KEY (`CompanyID` )
    REFERENCES `dta_test`.`tblCompany` (`idtblCompany` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblTestLocation`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblTestLocation` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblTestLocation` (
  `idtblTestLocation` INT NOT NULL AUTO_INCREMENT ,
  `CompanyID` INT NOT NULL ,
  `Name` VARCHAR(45) NOT NULL ,
  `Notes` VARCHAR(255) NULL ,
  `Enabled` TINYINT(1) NULL ,
  PRIMARY KEY (`idtblTestLocation`) ,
  INDEX `fkCompanyIDtblTestLocationtblCompany` (`CompanyID` ASC) ,
  UNIQUE INDEX `idtblTestLocation_UNIQUE` (`idtblTestLocation` ASC) ,
  CONSTRAINT `fkCompanyIDtblTestLocationtblCompany`
    FOREIGN KEY (`CompanyID` )
    REFERENCES `dta_test`.`tblCompany` (`idtblCompany` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblDeviceClass`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblDeviceClass` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblDeviceClass` (
  `idtblDeviceClass` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `Description` VARCHAR(255) NULL ,
  `DefaultHost` VARCHAR(30) NOT NULL ,
  `DefaultLogin` VARCHAR(30) NOT NULL ,
  `DefaultPassword` VARCHAR(30) NOT NULL ,
  `SoftwareURL` VARCHAR(255) NOT NULL ,
  `ScriptURL` VARCHAR(255) NOT NULL ,
  `ResultsURL` VARCHAR(255) NOT NULL ,
  `LastProducerSerial` VARCHAR(45) NULL ,
  `LastCustomerSerial` VARCHAR(45) NULL ,
  `MinProducerSerial` VARCHAR(45) NOT NULL ,
  `MaxProducerSerial` VARCHAR(45) NULL ,
  `MinCustomerSerial` VARCHAR(45) NOT NULL ,
  `MaxCustomerSerial` VARCHAR(45) NULL ,
  `Enabled` TINYINT(1) NOT NULL ,
  PRIMARY KEY (`idtblDeviceClass`) ,
  UNIQUE INDEX `idtblDeviceClass_UNIQUE` (`idtblDeviceClass` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblTestList`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblTestList` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblTestList` (
  `idtblTestList` INT NOT NULL AUTO_INCREMENT ,
  `Name` VARCHAR(45) NOT NULL ,
  `Description` VARCHAR(255) NULL ,
  `DeviceClassID` INT NOT NULL ,
  `CreationDate` DATETIME NOT NULL ,
  `Enabled` TINYINT(1) NOT NULL ,
  PRIMARY KEY (`idtblTestList`) ,
  UNIQUE INDEX `idtblTestList_UNIQUE` (`idtblTestList` ASC) ,
  INDEX `fkDeviceClasstblTestListtblDeviceClass` (`DeviceClassID` ASC) ,
  CONSTRAINT `fkDeviceClasstblTestListtblDeviceClass`
    FOREIGN KEY (`DeviceClassID` )
    REFERENCES `dta_test`.`tblDeviceClass` (`idtblDeviceClass` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblTestItem`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblTestItem` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblTestItem` (
  `idtblTestItem` INT NOT NULL AUTO_INCREMENT ,
  `TestListID` INT NOT NULL ,
  `Name` VARCHAR(45) NOT NULL ,
  `Description` VARCHAR(255) NULL ,
  `ExecutionOrder` INT NULL ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`idtblTestItem`) ,
  INDEX `fkTestListIDtblTestItemtblTestList` (`TestListID` ASC) ,
  UNIQUE INDEX `idtblTestItem_UNIQUE` (`idtblTestItem` ASC) ,
  CONSTRAINT `fkTestListIDtblTestItemtblTestList`
    FOREIGN KEY (`TestListID` )
    REFERENCES `dta_test`.`tblTestList` (`idtblTestList` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblDevice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblDevice` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblDevice` (
  `idtblDevice` INT NOT NULL AUTO_INCREMENT ,
  `ProducerSerialNumber` VARCHAR(45) NOT NULL ,
  `CustomerSerialNumber` VARCHAR(45) NULL ,
  `DeviceClassID` INT NULL ,
  `CreationDate` DATETIME NOT NULL ,
  `CreatorID` INT NOT NULL ,
  `LastTestDate` DATETIME NOT NULL ,
  `LastTestPersonID` INT NOT NULL ,
  PRIMARY KEY (`idtblDevice`) ,
  INDEX `fkDeviceClassIDtblDevicetblDeviceClass` (`DeviceClassID` ASC) ,
  UNIQUE INDEX `idtblDevice_UNIQUE` (`idtblDevice` ASC) ,
  INDEX `fkCreatorIDtblDevicetblEmployee` (`CreatorID` ASC) ,
  INDEX `fkLastTestPersonIDtblDevicetblEmployee` (`LastTestPersonID` ASC) ,
  UNIQUE INDEX `idtblDeviceProducerSerialNumberDeviceClass` (`DeviceClassID` ASC, `ProducerSerialNumber` ASC) ,
  CONSTRAINT `fkDeviceClassIDtblDevicetblDeviceClass`
    FOREIGN KEY (`DeviceClassID` )
    REFERENCES `dta_test`.`tblDeviceClass` (`idtblDeviceClass` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkCreatorIDtblDevicetblEmployee`
    FOREIGN KEY (`CreatorID` )
    REFERENCES `dta_test`.`tblEmployee` (`idtblEmployee` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkLastTestPersonIDtblDevicetblEmployee`
    FOREIGN KEY (`LastTestPersonID` )
    REFERENCES `dta_test`.`tblEmployee` (`idtblEmployee` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblTestListResult`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblTestListResult` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblTestListResult` (
  `idtblTestListResult` INT NOT NULL AUTO_INCREMENT ,
  `EmployeeID` INT NOT NULL ,
  `TestLocationID` INT NOT NULL ,
  `TestListID` INT NOT NULL ,
  `DeviceID` INT NOT NULL ,
  `Result` TINYINT(1) NOT NULL ,
  `ResultURL` VARCHAR(255) NULL ,
  `Notes` VARCHAR(255) NULL ,
  `CreationDate` DATETIME NOT NULL ,
  PRIMARY KEY (`idtblTestListResult`) ,
  INDEX `fkEmployeeIDtblTestListResulttblEmployee` (`EmployeeID` ASC) ,
  INDEX `fkTestLocationIDtblTestListResulttblTestLocation` (`TestLocationID` ASC) ,
  INDEX `fkTestListIDtblTestListResulttblTestList` (`TestListID` ASC) ,
  INDEX `fkDeviceIDtblTestListResulttblDevice` (`DeviceID` ASC) ,
  UNIQUE INDEX `idtblTestListResult_UNIQUE` (`idtblTestListResult` ASC) ,
  CONSTRAINT `fkEmployeeIDtblTestListResulttblEmployee`
    FOREIGN KEY (`EmployeeID` )
    REFERENCES `dta_test`.`tblEmployee` (`idtblEmployee` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkTestLocationIDtblTestListResulttblTestLocation`
    FOREIGN KEY (`TestLocationID` )
    REFERENCES `dta_test`.`tblTestLocation` (`idtblTestLocation` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkTestListIDtblTestListResulttblTestList`
    FOREIGN KEY (`TestListID` )
    REFERENCES `dta_test`.`tblTestList` (`idtblTestList` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkDeviceIDtblTestListResulttblDevice`
    FOREIGN KEY (`DeviceID` )
    REFERENCES `dta_test`.`tblDevice` (`idtblDevice` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dta_test`.`tblTestItemResult`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dta_test`.`tblTestItemResult` ;

CREATE  TABLE IF NOT EXISTS `dta_test`.`tblTestItemResult` (
  `idtblTestItemResult` INT NOT NULL AUTO_INCREMENT ,
  `TestItemID` INT NOT NULL ,
  `TestListResultID` INT NOT NULL ,
  `DeviceID` INT NOT NULL ,
  `Result` TINYINT(1) NOT NULL ,
  `Notes` VARCHAR(255) NULL ,
  `CreationDate` DATETIME NOT NULL ,
  PRIMARY KEY (`idtblTestItemResult`) ,
  INDEX `fkTestItemIDtblTestItemResulttblTestItem` (`TestItemID` ASC) ,
  INDEX `fkTestListResultIDtblTestItemResulttblTestListResult` (`TestListResultID` ASC) ,
  UNIQUE INDEX `idtblTestItemResult_UNIQUE` (`idtblTestItemResult` ASC) ,
  CONSTRAINT `fkTestItemIDtblTestItemResulttblTestItem`
    FOREIGN KEY (`TestItemID` )
    REFERENCES `dta_test`.`tblTestItem` (`idtblTestItem` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkTestListResultIDtblTestItemResulttblTestListResult`
    FOREIGN KEY (`TestListResultID` )
    REFERENCES `dta_test`.`tblTestListResult` (`idtblTestListResult` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- procedure try_login
-- -----------------------------------------------------

USE `dta_test`;
DROP procedure IF EXISTS `dta_test`.`try_login`;

DELIMITER $$
USE `dta_test`$$
CREATE PROCEDURE `try_login`(IN lg VARCHAR(20), IN pw VARCHAR(20))
BEGIN
SELECT COUNT(*) FROM tblemployee WHERE login=lg AND password=pw;
END
$$

DELIMITER ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
